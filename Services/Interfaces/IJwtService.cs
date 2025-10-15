using EnglishWordsNoteBook.Models;
using Microsoft.AspNetCore.Identity;

namespace EnglishWordsNoteBook.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(ApplicationUser user, IList<string> roles);
        string GenerateRefreshToken();

    }
}
