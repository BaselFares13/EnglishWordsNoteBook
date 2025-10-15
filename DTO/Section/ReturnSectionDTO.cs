using System.ComponentModel.DataAnnotations;

namespace EnglishWordsNoteBook.DTO.Section
{
    public class ReturnSectionDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string? Notes { get; set; }

        public DateTime Date { get; set; }
    }
}
