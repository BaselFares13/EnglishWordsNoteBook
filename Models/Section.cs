using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishWordsNoteBook.Models
{
    public class Section
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        public string? Notes { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual List<Word>? Words { get; set; } 
    }
}
