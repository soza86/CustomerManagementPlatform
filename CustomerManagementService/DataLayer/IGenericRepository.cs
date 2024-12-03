namespace CustomerManagementService.DataLayer
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAsIQueryable();

        Task<T?> GetByIdAsync(Guid id);

        Task<T> AddAsync(T entity);

        T Update(T entity);

        Task<bool> DeleteAsync(Guid id);

        Task SaveAsync();
    }
}
