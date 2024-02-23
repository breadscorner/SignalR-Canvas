using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Pictionary.Hubs
{
    public class PictionaryHub : Hub
    {
        private static string drawerConnectionId = "";

        public async Task SetDrawer(string connectionId)
        {
            if (drawerConnectionId == "" && connectionId != "")
            {
                drawerConnectionId = connectionId;
                await Clients.AllExcept(connectionId).SendAsync("UpdateDrawer", drawerConnectionId);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.ConnectionId == drawerConnectionId)
            {
                drawerConnectionId = "";
                await Clients.All.SendAsync("UpdateDrawer", drawerConnectionId);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendDataPoints(double x, double y) {
          Console.WriteLine("Received data points: " + x + ", " + y);
          // Send the data points to all clients except the sender
          await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveDataPoints", x, y);
        }

        // Method to broadcast drawing data to all clients
        public async Task SendDrawing(string drawingData)
        {
            await Clients.All.SendAsync("ReceiveDrawing", drawingData);
        }

        // Method to send the current word to the drawing player
        public async Task SendWordToDraw(string word)
        {
            await Clients.Caller.SendAsync("ReceiveWordToDraw", word);
        }

        // Method to send a guess to the drawing player
        public async Task SendGuess(string guess)
        {
            await Clients.Others.SendAsync("ReceiveGuess", guess);
        }
    }
}
