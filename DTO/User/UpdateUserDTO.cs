using System.ComponentModel.DataAnnotations;

namespace EnglishWordsNoteBook.DTO.User
{
    public class UpdateUserDTO
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? NewPassword { get; set; }
    }
}
