using EnglishWordsNoteBook.DTO.Section;
using EnglishWordsNoteBook.Models;

namespace EnglishWordsNoteBook.Services.Interfaces
{
    public interface ISectionService
    {
        public Task<ReturnSectionDTO?> AddAsync(int UserId, AddSectionDTO obj);
        public Task<ReturnSectionDTO?> UpdateAsync(int SectionId, UpdateSectionDTO obj);
        public Task<bool> DeleteAsync(int id);
        public Task<List<ReturnSectionDTO>> GetAllAsync();
        public Task<ReturnSectionDTO?> GetByIdAsync(int id);
        public Task<int> CountByUserIdAsync(int id);
        public Task<List<ReturnSectionDTO>> GetPageAsync(int skip, int take, int userId);
        public Task<List<ReturnSectionDTO>> GetByUserIdAsync(int id);
    }
}