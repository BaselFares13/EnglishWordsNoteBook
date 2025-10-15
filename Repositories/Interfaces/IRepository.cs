namespace EnglishWordsNoteBook.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        public Task<bool> AddAsync(T obj);
        public Task<bool> UpdateAsync(T obj);
        public Task<bool> DeleteAsync(int id);
        public Task<List<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(int id);

        public Task<int> CountByUserIdAsync(int id);

        public Task<List<T>> GetPageAsync(int Skip, int Take, int userId);
        public Task<List<T>> GetByUserIdAsync(int id);

        public Task<bool> SaveAsync();
    }
}
