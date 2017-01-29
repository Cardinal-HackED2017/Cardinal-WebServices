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
    public class EventsController : Controller
    {
        private readonly CardinalEventManager _eventManager;

        public EventsController(CardinalEventManager eventManager) 
        {
            _eventManager = eventManager;
        }

        [HttpGet("events")]
        public async Task Get() 
        {
            var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            var cardinalSocket = new CardinalWebSocket(this.GetCallingUserId(), webSocket);
            _eventManager.AddCardinalWebSocket(cardinalSocket);

            await cardinalSocket.ReceiveWhileConnectionIsOpen();
        }
    }
}