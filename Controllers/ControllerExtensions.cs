using cardinal_webservices.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace cardinal_webservices.Controllers
{
    public static class ControllerExtensions
    {
        public static User GetCallingUser(this Controller controller) 
        {
            string NUUID = "6f2241ae-da64-4aa8-a414-308d8f900057";
            
            return new User 
            {
                Id = NUUID,
                DisplayName = "Null",
                Email = "null@null.nil"
            };
        }
    }
}
