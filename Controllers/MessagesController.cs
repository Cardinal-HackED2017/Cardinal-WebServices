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
        public async Task<IActionResult> CreateMessage(Message message) 
        {
            message.MessageId = Guid.NewGuid().ToString();
            message.CreatedTime = DateTime.Now;
            message.UserId = null;
            message.MeetingId = null;
            await _cardinalDataService.UpsertMessageAsync(message);

            return Created("message", message);
        }
    }
}
