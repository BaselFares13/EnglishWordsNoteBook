using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnglishWordsNoteBook.DTO.Word
{
    public class UpdateWordDTO
    {

        [Required]
        public string WordValue { get; set; }

        [Required]
        public string Translation { get; set; }

        [Required]
        public int? SectionId { get; set; }
    }
}
