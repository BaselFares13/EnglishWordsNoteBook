using System.ComponentModel.DataAnnotations;

namespace EnglishWordsNoteBook.DTO.User
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,20}$",
           ErrorMessage = @"Password must be 8-20 characters, include uppercase, 
            lowercase, number, and special character.")]
        public string Password { get; set; }
    }
}
