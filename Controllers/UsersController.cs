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
        //The null uuid
        private string NUUID = "6f2241ae-da64-4aa8-a414-308d8f900057";
        private readonly ICardinalDataService _cardinalDataService;

        public UsersController(ICardinalDataService cardinalDataService) 
        {
            _cardinalDataService = cardinalDataService;
        }

        [HttpGet("users")]
        public IEnumerable<User> Get()
        {
            return _cardinalDataService.GetUsers();
        }

        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody]User user) 
        {
            user.Id = Guid.NewGuid().ToString();
            await _cardinalDataService.UpsertUserAsync(user);
            return Created("user", user);
        }
    }
}
