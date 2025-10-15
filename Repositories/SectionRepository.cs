using EnglishWordsNoteBook.Models;
using EnglishWordsNoteBook.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EnglishWordsNoteBook.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly DatabaseContext _context;

        public SectionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Section section)
        {
            bool result = true;
            try
            {
                var entityEntry = await _context.Sections.AddAsync(section);

            }
            catch (Exception ex) {
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

                _context.Sections.Remove(entity);
            }
            catch (Exception ex) {
                result = false;
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<bool> UpdateAsync(Section section)
        {
            bool result = true;
            try
            {
                var entityEntry = _context.Sections.Update(section);
            }
            catch (Exception ex) {
                result = false;
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<Section?> GetByIdAsync(int id)
        {
            Section? entity;
            try
            {
                entity = await _context.Sections.FirstOrDefaultAsync(s => s.Id == id);

            } catch(Exception ex) {
                entity = null;
                Console.WriteLine(ex.ToString());
            }

            return entity;
        }

        public async Task<List<Section>> GetAllAsync()
        {
            List<Section> result;
            try
            {
                result = await _context.Sections.OrderBy(w => w.Date).ToListAsync();
            }
            catch (Exception ex) {
                result = new List<Section>();
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<List<Section>> GetByUserIdAsync(int id)
        {
            List<Section> result;
            try
            {
                result = await _context.Sections.Where(s => s.UserId == id).OrderBy(w => w.Date).ToListAsync();
            }
            catch (Exception ex) {
                result = new List<Section>();
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<int> CountByUserIdAsync(int id)
        {
            int result;
            try
            {
                result = await _context.Sections.Where(s => s.UserId == id).CountAsync();

            }
            catch (Exception ex)
            {
                result = -1;
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        public async Task<List<Section>> GetPageAsync(int skip, int take, int userId)
        {
            List<Section> result;
            try
            {
                result = await _context.Sections.Where(s => s.UserId == userId).Skip(skip).Take(take).OrderBy(w => w.Date).ToListAsync();
            }
            catch (Exception ex) {
            
                result= new List<Section>(); ;
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
