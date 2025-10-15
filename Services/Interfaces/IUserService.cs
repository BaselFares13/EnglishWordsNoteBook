using EnglishWordsNoteBook.DTO.User;

namespace EnglishWordsNoteBook.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ReturnedUserDTO?> AddAsync(AddUserDTO obj);
        public Task<ReturnedUserDTO?> UpdateAsync(UpdateUserDTO obj);
        public Task<bool> DeleteAsync(int id);
    }
}
