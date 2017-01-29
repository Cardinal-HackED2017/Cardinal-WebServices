using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cardinal_webservices.Data;
using cardinal_webservices.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace cardinal_webservices.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly ICardinalDataService _cardinalDataService;

        public MeetingsController(ICardinalDataService cardinalDataService) 
        {
            _cardinalDataService = cardinalDataService;
        }

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
    }
}