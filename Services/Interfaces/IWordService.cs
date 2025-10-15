using EnglishWordsNoteBook.DTO.Section;
using EnglishWordsNoteBook.DTO.Word;
using EnglishWordsNoteBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnglishWordsNoteBook.Services.Interfaces
{
    public interface IWordService
    {
        public Task<ReturnWordDTO?> AddAsync(AddWordDTO obj);
        public Task<ReturnWordDTO?> UpdateAsync(int userId, UpdateWordDTO obj);
        public Task<bool> DeleteAsync(int id);
        public Task<List<ReturnWordDTO>> GetAllAsync();
        public Task<ReturnWordDTO?> GetByIdAsync(int id);
        public Task<int> CountByUserIdAsync(int id);
        public Task<List<ReturnWordDTO>> GetPageAsync(int skip, int take, int userId);
        public Task<List<ReturnWordDTO>> GetByUserIdAsync(int id);
        public Task<List<ReturnWordDTO>> GetBySectionIdAsync(int id);
        public Task<int> CountBySectionIdAsync(int id);
        public Task<List<ReturnWordDTO>> SearchByUserAndSubstringAsync(int id, string substr);
        public Task<List<ReturnWordDTO>> GetWordsWithoutPronunciationByUserId(int id);
        public Task<string?> UpdatePronounciationAudioFileByWordId(int Id, UploadPronounciationDTO PronounciationDTO);

    }
}
