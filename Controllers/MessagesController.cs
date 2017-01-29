using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cardinal_webservices.Data;
using cardinal_webservices.DataModels;
using cardinal_webservices.WebSockets;
using Microsoft.AspNetCore.Mvc;

namespace cardinal_webservices.Controllers
{
    public class MessagesController : Controller
    {
        //The null uuid
        private string NUUID = "6f2241ae-da64-4aa8-a414-308d8f900057";
        private readonly ICardinalDataService _cardinalDataService;
        private readonly CardinalEventManager _eventManager;

        public MessagesController(ICardinalDataService cardinalDataService, CardinalEventManager eventManager) 
        {
            _cardinalDataService = cardinalDataService;
            _eventManager = eventManager;
        }

        [HttpGet("meetings/{meetingid}/messages")]
        public IEnumerable<Message> Get(string meetingid)
        {
            return _cardinalDataService.GetMessages()
                                       .Where(x => x.MeetingId.Equals(meetingid))
                                       .ToList();
        }

        [HttpPost("meetings/{meetingid}/messages")]
        public async Task<IActionResult> CreateMessage([FromBody]Message message, string meetingid) 
        {
            message.MessageId = Guid.NewGuid().ToString();
            message.CreatedTime = DateTime.Now;
            message.UserId = this.GetCallingUserId();
            message.MeetingId = meetingid;
            await _cardinalDataService.UpsertMessageAsync(message);

            _eventManager.OnMessageCreated(message);

            return Created("message", message);
        }
    }
}
