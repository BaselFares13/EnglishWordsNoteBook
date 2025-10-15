using EnglishWordsNoteBook.Models;

namespace EnglishWordsNoteBook.Repositories.Interfaces
{
    public interface IWordRepository : IRepository<Word>
    {
        public Task<List<Word>> GetBySectionIdAsync(int id);
        public Task<int> CountBySectionIdAsync(int id);
        public Task<List<Word>> SearchByUserAndSubstringAsync(int id, string substr);
        public Task<List<Word>> GetWordsWithoutPronunciationByUserId(int id);
        public Task<bool> UpdatePronounciationAudioUrlByWordId(int id, string url);
    }
}
