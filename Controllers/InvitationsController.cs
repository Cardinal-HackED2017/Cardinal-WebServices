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
    public class InvitationsController : Controller
    {
        private readonly ICardinalDataService _cardinalDataService;

        public InvitationsController(ICardinalDataService cardinalDataService) 
        {
            _cardinalDataService = cardinalDataService;
        }

        [HttpGet("invitations")]
        public IEnumerable<InvitationModel> GetMeetings()
        {
            return _cardinalDataService.GetInvitationsForUser(this.GetCallingUserId())
                                       .Select(i => new InvitationModel(i))
                                       .ToList();
        }
    }
}