namespace Pictionary.Models
{
    public class Guess
    {
        public int Id { get; set; }
        public int DrawEntryId { get; set; }
        public required string GuesserName { get; set; }
        public required string GuessedDrawing { get; set; } 
        public bool IsCorrect { get; set; } 
    }
}
