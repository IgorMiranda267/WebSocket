using System.Net.WebSockets;

class Program
{
    static async Task Main(string[] args)
    {
        // Site para teste https://www.piesocket.com/websocket-tester 
        // cria o URI do WebSocket
        Uri uri = new Uri("wss://demo.piesocket.com/v3/channel_123?api_key=VCXCEuvhGcBDP7XhiJJUDvR1e1D3eiVjgZ9VRiaV&notify_self");
        
        // cria um cliente WebSocket
        using (ClientWebSocket client = new ClientWebSocket())
        {
            // conecta ao servidor WebSocket
            await client.ConnectAsync(uri, CancellationToken.None);

            Console.WriteLine("Conectado ao servidor WebSocket!");
            string message = "Olá, servidor WebSocket!";
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);

            // envia uma mensagem ao servidor WebSocket
            await client.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);

            // aguarda mensagens do servidor WebSocket
            byte[] buffer = new byte[1024];
            while (true)
            {
                // recebe a mensagem do servirdor WebSocket
                WebSocketReceiveResult result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string receivedMessage = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine("Mensagem recebida do servidor WebSocket: {0}", receivedMessage);
                }
            }
        }
    }
}


