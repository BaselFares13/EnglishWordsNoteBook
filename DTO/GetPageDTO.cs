using System.ComponentModel.DataAnnotations;

namespace EnglishWordsNoteBook.DTO
{
    public class GetPageDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Page must be 1 or greater")]
        public int PageNum { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ItemsPerPage must be 1 or greater")]
        public int ItemsPerPage { get; set; }
    }
}
