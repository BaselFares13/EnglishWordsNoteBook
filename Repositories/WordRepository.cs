using System.Security.AccessControl;
using EnglishWordsNoteBook.Models;
using EnglishWordsNoteBook.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EnglishWordsNoteBook.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly DatabaseContext _context;

        public WordRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Word word)
        {
            bool result = true;
            try
            {
                var entityEntry = await _context.Words.AddAsync(word);
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = true;
            try
            {
                var entity = await GetByIdAsync(id);

                if (entity == null) return false;

                _context.Words.Remove(entity);
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<bool> UpdateAsync(Word word)
        {
            bool result = true ;

            try
            {
                var entityEntry = _context.Words.Update(word);
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<Word?> GetByIdAsync(int id)
        {
            Word? entity;
            try
            {
                entity = await _context.Words.FirstOrDefaultAsync(s => s.Id == id);

            }
            catch (Exception ex)
            {
                entity = null;
                Console.WriteLine(ex.ToString());
            }

            return entity;
        }

        public async Task<List<Word>> GetAllAsync()
        {
            List<Word> result;
            try
            {
                result = await _context.Words.OrderBy(w => w.Date).ToListAsync();
            }
            catch (Exception ex)
            {
                result = new List<Word>();
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<List<Word>> GetByUserIdAsync(int id)
        {
            List<Word> result;
            try
            {
                result = await _context.Words
                    .Where(s => s.Section.UserId == id)
                    .OrderBy(w => w.Date).ToListAsync();
            }
            catch (Exception ex)
            {
                result = new List<Word>();
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<int> CountByUserIdAsync(int id)
        {
            int result;
            try
            {
                result = await _context.Words.Where(w => w.Section.UserId == id).CountAsync();

            }
            catch (Exception ex)
            {
                result = -1;
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<List<Word>> GetPageAsync(int skip, int take, int userId)
        {
            List<Word> result;
            try
            {
                result = await _context.Words.Where(w => w.Section.UserId == userId).Skip(skip).
                    Take(take).OrderBy(w => w.Date).ToListAsync();
            }
            catch (Exception ex)
            {

                result = new List<Word>();
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<List<Word>> GetBySectionIdAsync(int id)
        {
            List<Word> result;
            try
            {
                result = await _context.Words.Where(w => w.SectionId == id).OrderBy(w => w.Date).ToListAsync();
            }
            catch (Exception ex)
            {

                result = new List<Word>();
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<int> CountBySectionIdAsync(int id)
        {
            int result;
            try
            {
                result = await _context.Words.Where(w => w.SectionId == id).CountAsync();
            }
            catch (Exception ex)
            {

                result = -1;
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<List<Word>> SearchByUserAndSubstringAsync(int id, string substr)
        {
            List<Word> result;
            try
            {
                result = await _context.Words
                    .Where(w => w.Section.UserId == id && EF.Functions.Like(w.WordValue.ToLower(), $"%{substr.ToLower()}%")).ToListAsync();
            }
            catch (Exception ex)
            {

                result = new List<Word>();
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<List<Word>> GetWordsWithoutPronunciationByUserId(int id)
        {
            List<Word> result;
            try
            {
                result = await _context.Words
                    .Where(w => w.PronunciationAudioFileUrl == null).ToListAsync();
            }
            catch (Exception ex)
            {

                result = new List<Word>();
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<bool> UpdatePronounciationAudioUrlByWordId(int id, string url)
        {
            bool result = true;
            try
            {
                var word = await _context.Words.SingleOrDefaultAsync(w => w.Id == id);
                if (word == null) result = false;
                else
                {
                    word.PronunciationAudioFileUrl = url;
                }
            }
            catch (Exception ex) {
                result = false;
                Console.WriteLine(ex.ToString());
            }

            return result;
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
