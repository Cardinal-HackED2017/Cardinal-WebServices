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
        private readonly MeetingTimesCalculator _meetingTimesCalculator;

        public MeetingsController(ICardinalDataService cardinalDataService, MeetingTimesCalculator meetingsTimeCalculator) 
        {
            _cardinalDataService = cardinalDataService;
            _meetingTimesCalculator = meetingsTimeCalculator;
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
            meeting.CreatorId = this.GetCallingUserId();
            meeting.CreatedTime = DateTime.Now;

            await _cardinalDataService.UpsertMeetingAsync(meeting);
            await JoinMeeting(meeting.Id, meeting.CreatorId);

            return Created("meeting", meeting);
        }

        private async Task JoinMeeting(string meetingId, string userId) 
        {
            var meetingParticipation = new MeetingParticipation 
            {
                MeetingId = meetingId,
                UserId = userId
            };

            await _cardinalDataService.UpsertMeetingParticipationAsync(meetingParticipation);
            _meetingTimesCalculator.ProcessUserJoinMeeting(meetingId, userId, this.GetAuthToken());
        }
    }
}