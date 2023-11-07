using ECommerce.Domain.Entities.Common;

namespace ECommerce.Application.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
{
    Task<bool> AddAsync(T entity);
    bool Add(T entity);
    Task<bool> AddRangeAsync(List<T> entities);
    bool Update(T entity);
    Task<int> SaveAsync();
    int Save();
}