using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Pictionary.Models; // Assuming DrawEntry is defined in this namespace

namespace Pictionary.Hubs
{
  public class PictionaryHub : Hub
  {
    public static DrawEntry ActiveDrawEntry { get; set; } = new DrawEntry();

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
