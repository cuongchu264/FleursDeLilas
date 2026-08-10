namespace FleursDeLilas.API.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
