using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cardinal_webservices.Data;
using cardinal_webservices.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace cardinal_webservices.Controllers
{
    public class MessagesController : Controller
    {
        //The null uuid
        private string NUUID = "6f2241ae-da64-4aa8-a414-308d8f900057";
        private readonly ICardinalDataService _cardinalDataService;

        public MessagesController(ICardinalDataService cardinalDataService) 
        {
            _cardinalDataService = cardinalDataService;
        }

        [HttpGet("messages")]
        public IEnumerable<Message> Get()
        {
            return _cardinalDataService.GetMessages();
        }

        [HttpPost("messages")]
        public async Task<IActionResult> CreateMessage([FromBody]Message message) 
        {
            message.MessageId = Guid.NewGuid().ToString();
            message.CreatedTime = DateTime.Now;
            message.UserId = NUUID;
            message.MeetingId = NUUID;
            await _cardinalDataService.UpsertMessageAsync(message);

            return Created("message", message);
        }
    }
}
