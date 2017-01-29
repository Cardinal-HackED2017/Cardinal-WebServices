using System.Collections.Generic;
using System.Net.WebSockets;
using cardinal_webservices.Data;

namespace cardinal_webservices.WebSockets 
{
    public class CardinalEventManager 
    {
        private readonly IEnumerable<CardinalWebSocket> _sockets;
        private readonly ICardinalDataService cardinalDataService;

        public CardinalEventManager(ICardinalDataService cardinalDataService) 
        {
            _sockets = new List<CardinalWebSocket>();
        }

        public void OnMessageCreated() 
        {

        }
    }
}