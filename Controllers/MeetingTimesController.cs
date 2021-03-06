using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cardinal_webservices.Data;
using cardinal_webservices.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace cardinal_webservices.Controllers
{
    public class MeetingTimesController : Controller
    {
        private readonly ICardinalDataService _cardinalDataService;
        public MeetingTimesController(ICardinalDataService cardinalDataService) 
        {
            _cardinalDataService = cardinalDataService;
        }

        [HttpGet("meetings/{meetingid}/suggested-times")]
        public IEnumerable<MeetingTime> GetMeetingTimes(string meetingid)
        {
            return _cardinalDataService.GetMeetingTimes()
                                       .Where(x => x.MeetingId.Equals(meetingid))
                                       .ToList();
        }

        [HttpPost("meetings/{meetingid}/suggested-times")]
        public async Task<IActionResult> JoinMeeting([FromBody] MeetingTime meetingTime, string meetingid) 
        {
            meetingTime.MeetingId = meetingid;
            meetingTime.Id = Guid.NewGuid().ToString();

            await _cardinalDataService.UpsertMeetingTimeAsync(meetingTime);

            return Created("meetingTime", meetingTime);
        }
    }
}