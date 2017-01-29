using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;

namespace cardinal_webservices.GoogleCalendar
{
    public class GoogleCalendarService
    {
        private readonly string _token;
        public const string GOOGLE_CALENDAR_API_BASE_URL = "https://www.googleapis.com/calendar/v3";

        public GoogleCalendarService(string token) 
        {
            _token = token;
        }

        public async Task<IEnumerable<Calendar>> GetCalendarsAsync() 
        {
            var httpClient = GetHttpClient();
            var calendarsListResponse = httpClient.GetAsync("/users/me/calendarList");
            var resultJson = await calendarsListResponse.Result.Content.ReadAsStringAsync();
            Console.WriteLine(resultJson);

            return JsonConvert.DeserializeObject<IEnumerable<Calendar>>(resultJson);
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(string calendarId) 
        {
            var httpClient = GetHttpClient();
            var calendarsListResponse = httpClient.GetAsync($"/calendars/{calendarId}/events");
            var resultJson = await calendarsListResponse.Result.Content.ReadAsStringAsync();
            Console.WriteLine(resultJson);

            return JsonConvert.DeserializeObject<IEnumerable<Event>>(resultJson);;
        }

        public HttpClient GetHttpClient() 
        {
            return new HttpClient(new AuthenticatingHttpClientHandler(_token))
            {
                BaseAddress = new Uri(GOOGLE_CALENDAR_API_BASE_URL)
            };
        }
    }

    public class AuthenticatingHttpClientHandler : HttpClientHandler 
    {
        private readonly string _token;

        public AuthenticatingHttpClientHandler(string token) 
        {
            _token = token;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", $"Bearer {_token}");
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }

}