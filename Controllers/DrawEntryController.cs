using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Pictionary.Hubs;
using Pictionary.Models;
using System;

namespace Pictionary.Controllers
{
  [Route("api/draw/[controller]")]
  [ApiController]
  public class DrawEntryController : ControllerBase
  {
    private readonly IHubContext<PictionaryHub> _hubContext;

    public DrawEntryController(IHubContext<PictionaryHub> hubContext)
    {
      _hubContext = hubContext;
    }

    [HttpPut("UpdateDrawing")]
    public async Task<IActionResult> UpdateDrawing([FromBody] DrawEntry drawEntry)
    {
      // Check if the model state is valid
      if (!ModelState.IsValid)
      {
        // If model state is not valid, return BadRequest with validation errors
        return BadRequest(ModelState);
      }

      // Your logic to update the existing drawing session
      // For example:
      // Update the currently active drawing session
      PictionaryHub.ActiveDrawEntry = drawEntry;

      // Notify clients that a drawing session has been updated
      // You'd pass whatever relevant drawing data is necessary, which might include
      // the new drawing state or a diff from the previous state.
      await _hubContext.Clients.All.SendAsync("DrawingSessionUpdated", drawEntry);

      // Return a success response
      return Ok("Drawing updated.");
    }
    // POST: api/DrawEntry/StartDrawing
    [HttpPost("SaveDrawing")]

    public async Task SaveDrawing(string drawingData)
    {
      // Broadcast drawing to all clients
      await Clients.All.SendAsync("ReceiveDrawing", drawingData);
    }
      // Check if the model state is valid
      if (!ModelState.IsValid)
      {
        // If model state is not valid, return BadRequest with validation errors
        return BadRequest(ModelState);
  }

  // Your logic to start the drawing session
  // For example:
  // Set the currently active drawing session
  PictionaryHub.ActiveDrawEntry = drawEntry;

      // Notify clients that a drawing session has started
      _hubContext.Clients.All.SendAsync("DrawingSessionStarted", drawEntry.ParticipantName);

      // Return a success response
      return Ok("Drawing session started.");
}

// POST: api/DrawEntry/EndDrawing
[HttpPost("EndDrawing")]
public IActionResult EndDrawing()
{
  // Your logic to end the drawing session
  // For example:
  // Clear the currently active drawing session
  PictionaryHub.ActiveDrawEntry = null;

  // Notify clients that the drawing session has ended
  _hubContext.Clients.All.SendAsync("DrawingSessionEnded");

  // Return a success response
  return Ok("Drawing session ended.");
}
  }
}
