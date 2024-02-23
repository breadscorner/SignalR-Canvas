using Microsoft.AspNetCore.SignalR;
using Pictionary.Models;

namespace Pictionary.Hubs
{
    public class Pictionary : Hub
    {
        private static DrawEntry? ActiveDrawEntry { get; set; }

        public async Task SendDrawingSessionUpdated(DrawEntry drawEntry)
        {
            ActiveDrawEntry = drawEntry;
            await Clients.All.SendAsync("DrawingSessionUpdated", drawEntry);
        }

        public async Task SendDrawingSessionStarted(string participantName)
        {
            await Clients.All.SendAsync("DrawingSessionStarted", participantName);
        }

        public async Task SendDrawingSessionEnded()
        {
            await Clients.All.SendAsync("DrawingSessionEnded");
        }

        public async Task SendCorrectGuess(string guesserName)
        {
            await Clients.All.SendAsync("CorrectGuess", guesserName);
        }
    }
}
