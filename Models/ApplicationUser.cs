using Microsoft.AspNetCore.Identity;

namespace EnglishWordsNoteBook.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationTime { get; set; }
        public virtual List<Section>? Sections { get; set; }

    }
}
