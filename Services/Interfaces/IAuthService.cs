using EnglishWordsNoteBook.DTO.User;

namespace EnglishWordsNoteBook.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> Regsiter(AddUserDTO obj);
        public Task<ReturnAuthDTO> Login(LoginUserDTO obj);
        public Task<bool> Logout(int userId);
        public Task<ReturnAuthDTO> Refresh(string refreshToken);
    }
}
