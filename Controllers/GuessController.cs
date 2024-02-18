using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Pictionary.Hubs;
using Pictionary.Models;
using System;

namespace Pictionary.Controllers
{
    [Route("api/guess/[controller]")]
    [ApiController]
    public class GuessController : ControllerBase
    {
        private readonly IHubContext<PictionaryHub> _hubContext;

        public GuessController(IHubContext<PictionaryHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // POST: api/Guess/MakeGuess
        [HttpPost("MakeGuess")]
        public IActionResult MakeGuess([FromBody] Guess guess)
        {
            // Check if a drawing session is active
            if (PictionaryHub.ActiveDrawEntry == null)
            {
                return NotFound("No drawing session is active.");
            }

            // Check if the guess is correct
            if (guess.GuessedDrawing == PictionaryHub.ActiveDrawEntry.Drawing)
            {
                // Notify clients that a correct guess has been made
                _hubContext.Clients.All.SendAsync("CorrectGuess", guess.GuesserName);

                return Ok($"Congratulations, {guess.GuesserName}! Your guess is correct.");
            }
            else
            {
                return Ok($"Sorry, {guess.GuesserName}, your guess is incorrect.");
            }
        }
    }
}
