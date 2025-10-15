using System.ComponentModel.DataAnnotations;

namespace EnglishWordsNoteBook.DTO.Word
{
    public class UploadPronounciationDTO
    {
        [Required]
        public IFormFile PronounciationAudioFile { get; set; }
    }
}
