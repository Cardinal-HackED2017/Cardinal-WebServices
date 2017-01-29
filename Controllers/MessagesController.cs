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

        // GET api/values
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
            message.UserId = NUUID;
            message.MeetingId = NUUID;
            await _cardinalDataService.UpsertMessageAsync(message);

            return Created("message", message);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
