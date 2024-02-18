using System;
using System.ComponentModel.DataAnnotations;

namespace Pictionary.Models
{
    public class DrawEntry
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Participant name is required")]
        public string ParticipantName { get; set; }

        [Required(ErrorMessage = "Drawing is required")]
        public string Drawing { get; set; }

        public DateTime EntryTime { get; set; }

        public DrawEntry()
        {
            ParticipantName = "";
            Drawing = "";
        }
    }
}
