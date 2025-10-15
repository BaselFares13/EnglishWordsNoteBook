using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishWordsNoteBook.Models
{
    public class Word
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string WordValue { get; set; }
        
        [Required]
        public string Translation { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string? PronunciationAudioFileUrl { get; set; }

        [Required]
        [ForeignKey("Section")]
        public int SectionId { get; set; }

        public virtual Section Section { get; set; }
    }
}
