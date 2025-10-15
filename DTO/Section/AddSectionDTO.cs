using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnglishWordsNoteBook.DTO.Section
{
    public class AddSectionDTO
    {
        [Required]
        public string Title { get; set; }
        public string? Notes { get; set; }
    }
}