using System.ComponentModel.DataAnnotations;
using EnglishWordsNoteBook.HelpingClasses;

namespace EnglishWordsNoteBook.DTO.User
{
    public class AddUserDTO
    {
        [Required]
        public string Username { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,20}$",
            ErrorMessage = @"Password must be 8-20 characters, include uppercase, 
            lowercase, number, and special character.")]
        public string Password { get; set; }

        public string RoleName { get; set; } = RoleNames.User;
    }
}
