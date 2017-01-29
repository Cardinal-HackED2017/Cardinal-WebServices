using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cardinal_webservices.Data;
using cardinal_webservices.DataModels;
using cardinal_webservices.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace cardinal_webservices.Controllers
{
    public class InvitationsController : Controller
    {
        private readonly ICardinalDataService _cardinalDataService;
        private readonly MeetingTimesCalculator _meetingTimesCalculator;

        public InvitationsController(ICardinalDataService cardinalDataService, MeetingTimesCalculator meetingsTimeCalculator) 
        {
            _cardinalDataService = cardinalDataService;
            _meetingTimesCalculator = meetingsTimeCalculator;
        }

        [HttpGet("invitations")]
        public IEnumerable<InvitationModel> GetInvitations()
        {
            return _cardinalDataService.GetInvitationsForUser(this.GetCallingUserId())
                                       .Select(i => new InvitationModel(i, _cardinalDataService.GetMeetingForInvitation(i.InvitationId)))
                                       .ToList();
        }

        [HttpPost("invitations")]
        public async Task<IActionResult> SendInvitations([FromBody] InvitationRequest invitationsRequest)  
        {
            var inviteTasks = invitationsRequest.Invitations
                                                .Select(i => CreateInvitation(invitationsRequest.MeetingId, i))
                                                .Select(i => _cardinalDataService.UpsertInvitationAsync(i));
            
            await Task.WhenAll(inviteTasks);

            return Created("invitations", invitationsRequest);
        }

        private Invitation CreateInvitation(string meetingId, string userId) 
        {
            return new Invitation 
            {
                InvitationId = Guid.NewGuid().ToString(),
                MeetingId = meetingId,
                UserId = userId
            };
        }
        
        [HttpPost("invitations/{invitationId}")]
        public async Task<IActionResult> AcceptInvitation(string invitationId) 
        {
            var invitation = _cardinalDataService.GetInvitationsForUser(this.GetCallingUserId())
                                                 .Where(i => i.UserId == this.GetCallingUserId())
                                                 .First();

            var meetingParticipation = new MeetingParticipation 
            {
                MeetingId = invitation.MeetingId,
                UserId = invitation.UserId
            };
            
            await _cardinalDataService.UpsertMeetingParticipationAsync(meetingParticipation);
            await _cardinalDataService.DeleteInvitationAsync(invitationId);

            _meetingTimesCalculator.ProcessUserJoinMeeting(invitation.MeetingId, invitation.UserId, this.GetAuthToken());

            return NoContent();
        }
    }

    public class InvitationRequest 
    {
        [JsonProperty("meetingId")]
        public string MeetingId { get; set; }
        [JsonProperty("invitations")]
        public IEnumerable<string> Invitations { get; set; }
    }
}