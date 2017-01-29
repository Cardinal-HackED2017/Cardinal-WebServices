using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cardinal_webservices.WebSockets 
{
    public class MessageWebSocket 
    {
        private readonly WebSocket _websocket;
        public MessageWebSocket(WebSocket webSocket) 
        {
            _websocket = webSocket;
        }

        public async Task SendAsync(string message)
        {
            if (_websocket.State == WebSocketState.Open) 
            {
                var bytes = Encoding.UTF8.GetBytes(message);
                var outgoing = new ArraySegment<byte>(bytes);
                await _websocket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}