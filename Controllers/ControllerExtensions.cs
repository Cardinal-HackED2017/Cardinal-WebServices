using cardinal_webservices.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace cardinal_webservices.Controllers
{
    public static class ControllerExtensions
    {
        public static string GetCallingUserId(this Controller controller) 
        {
            try 
            {
                return controller.HttpContext.Request.Headers["e-mail"];
            }
            catch
            {
                return "6f2241ae-da64-4aa8-a414-308d8f900057";
            }
        }
    }
}
