using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnglishWordsNoteBook.DTO.Word
{
    public class ReturnWordDTO
    {
        public int Id { get; set; }

        public string WordValue { get; set; }

        public string Translation { get; set; }
        public DateTime Date { get; set; }
        public string? PronunciationAudioFileUrl { get; set; }
        public int? SectionId { get; set; }

    }
}
