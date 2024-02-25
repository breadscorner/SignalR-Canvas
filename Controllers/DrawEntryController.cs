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

// not used yet
        public async Task SendCorrectGuess(string guesserName)
        {
            await Clients.All.SendAsync("CorrectGuess", guesserName);
        }
    }
}
