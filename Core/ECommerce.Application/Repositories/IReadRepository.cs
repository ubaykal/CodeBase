using System.Linq.Expressions;
using ECommerce.Application.ViewModels;
using ECommerce.Domain.Entities.Common;

namespace ECommerce.Application.Repositories;

public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
{
    // IQueryable<T> GetAll(bool tracking = true);
    // IQueryable<T> GetAll(Expression<Func<T, bool>> method, bool tracking = true);
    // Task<T> GetAsync(Expression<Func<T, bool>> method, bool tracking = true);
    // Task<T> GetByIdAsync(string id, bool tracking = true);
    //
    IQueryable<T> GetAll();
    IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate, params Expression<Func<T,
        object>>[] includeProperties);

    Task<T>  GetAsync(Expression<Func<T?, bool>> predicate = null, params Expression<Func<T, object>>[]
        includeProperties);

    T Get(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includeProperties);

}