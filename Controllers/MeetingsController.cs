using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cardinal_webservices.Data;
using cardinal_webservices.DataModels;
using cardinal_webservices.Models;
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

        private MeetingModel ConvertToMeetingModel(Meeting meeting) 
        {
            var usersForMeeting = _cardinalDataService.GetUsersForMeeting(meeting.Id);
            var timesForMeeting = _cardinalDataService.GetMeetingTimesForMeeting(meeting.Id);

            return new MeetingModel(meeting, usersForMeeting, timesForMeeting);
        }

        [HttpGet("meetings")]
        public IEnumerable<MeetingModel> GetMeetings()
        {
            return _cardinalDataService.GetMeetingsForUser(this.GetCallingUserId())
                                       .Select(ConvertToMeetingModel)
                                       .ToList();
        }

        [HttpPost("meetings")]
        public async Task<IActionResult> CreateMeeting([FromBody] Meeting meeting) 
        {
            meeting.Id = Guid.NewGuid().ToString();
            meeting.CreatedTime = DateTime.Now;

            await _cardinalDataService.UpsertMeetingAsync(meeting);

            return Created("meeting", meeting);
        }
    }
}