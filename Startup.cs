using cardinal_webservices.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using cardinal_webservices.WebSockets;

namespace cardinal_webservices
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("secrets.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connString = Configuration["connSecret"];
            services.AddDbContext<CardinalDbContext>(options =>
                options.UseNpgsql(
                    connString,
                    b => b.MigrationsAssembly("AspNet5MultipleProject")
                )
            );

            services.AddScoped<ICardinalDataService, CardinalDataService>();
            services.AddSingleton<CardinalEventManager>();
            services.AddSingleton<MeetingTimesCalculator>();

            // Add framework services.
            services.AddMvc();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder =>
                builder.AllowAnyHeader()
                       .AllowAnyOrigin()
                       .AllowAnyMethod()
            );
            app.UseWebSockets();
            app.UseMvc();
        }
    }
}
