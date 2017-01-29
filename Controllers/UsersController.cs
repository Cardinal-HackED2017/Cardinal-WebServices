using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cardinal_webservices.Data;
using cardinal_webservices.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace cardinal_webservices.Controllers
{
    public class UsersController : Controller
    {
        private readonly ICardinalDataService _cardinalDataService;

        public UsersController(ICardinalDataService cardinalDataService) 
        {
            _cardinalDataService = cardinalDataService;
        }

        public bool DoesUserExist(string userId) 
        {
            return _cardinalDataService.GetUsers()
                                       .Any(u => u.Id == userId);
        }

        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody]User user) 
        {
            var userId = this.GetCallingUserId();

            if (DoesUserExist(userId)) 
            {
                return NoContent();
            }

            user.Id = this.GetCallingUserId();
            await _cardinalDataService.UpsertUserAsync(user);
            return Created("user", user);
        }

        [HttpOptions("users")]
        public IActionResult Options() 
        {
            HttpContext.Response.Headers.Add("Allow", "OPTIONS, GET, POST");
            return Ok();
        }
    }
}
