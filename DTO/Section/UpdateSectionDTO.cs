using System.ComponentModel.DataAnnotations;

namespace EnglishWordsNoteBook.DTO.Section
{
    public class UpdateSectionDTO
    {

        [Required]
        public string Title { get; set; }
        public string? Notes { get; set; }
    }
}
