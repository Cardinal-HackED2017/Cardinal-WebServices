using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cardinal_webservices.Data;
using cardinal_webservices.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace cardinal_webservices.Controllers
{
    public class ValuesController : Controller
    {
        private readonly ICardinalDataService _cardinalDataService;

        public ValuesController(ICardinalDataService cardinalDataService) 
        {
            _cardinalDataService = cardinalDataService;
        }

        // GET api/values
        [HttpGet("meetings")]
        public IEnumerable<Meeting> Get()
        {
            return _cardinalDataService.GetMeetings();
        }

        [HttpPost("meetings")]
        public async Task<IActionResult> CreateMeeting(Meeting meeting) 
        {
            meeting.Id = Guid.NewGuid().ToString();
            meeting.CreatedTime = DateTime.Now;

            await _cardinalDataService.UpsertMeetingAsync(meeting);

            return Created("meeting", meeting);
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
