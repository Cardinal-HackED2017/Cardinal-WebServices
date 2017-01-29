using System.Net.WebSockets;

namespace cardinal_webservices.WebSockets 
{
    public class CardinalWebSocket : MessageWebSocket 
    {
        public string UserId { get; private set; }
        
        public CardinalWebSocket(string userId, WebSocket webSocket) : base(webSocket)
        {
            UserId = userId;
        }
    }
}