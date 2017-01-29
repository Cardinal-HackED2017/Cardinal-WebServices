using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cardinal_webservices.Data;
using cardinal_webservices.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace cardinal_webservices.Controllers
{
    public class MeetingParticipationController : Controller
    {
        private readonly ICardinalDataService _cardinalDataService;
        //The null uuid
        private string NUUID = "6f2241ae-da64-4aa8-a414-308d8f900057"; // tier 1
        public MeetingParticipationController(ICardinalDataService cardinalDataService) 
        {
            _cardinalDataService = cardinalDataService;
        }

        [HttpGet("meetings/{meetingid}/participants")]
        public IEnumerable<User> GetMeetingParticipation(string meetingid)
        {
            var userIds =  _cardinalDataService.GetMeetingParticipations()
                                               .Select(p => p.UserId);

            return _cardinalDataService.GetUsers()
                                       .Where(u => userIds.Contains(u.Id))
                                       .ToList();
        }

        [HttpPost("meetings/{meetingid}/join")]
        public async Task<IActionResult> JoinMeeting([FromBody] MeetingParticipation meetingParticipation, string meetingid) 
        {
            meetingParticipation.MeetingId = meetingid;
            meetingParticipation.UserId = this.GetCallingUserId();

            await _cardinalDataService.UpsertMeetingParticipationAsync(meetingParticipation);

            return Created("meeting", meetingParticipation);
        }
    }
}