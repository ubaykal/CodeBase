using System.Linq.Expressions;
using ECommerce.Application.Repositories;
using ECommerce.Application.ViewModels;
using ECommerce.Domain.Entities.Common;
using ECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly ECommerceDbContext _context;

    public ReadRepository(ECommerceDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll()
    {
        IQueryable<T> query = Table.AsTracking();

        return query;
    }
    
    public IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate, params Expression<Func<T,
        object>>[] includeProperties)
    {
        IQueryable<T> query = Table.AsTracking();
        if (predicate != null)
            query = query.Where(predicate);

        if (includeProperties.Any())
            foreach (var item in includeProperties)
                query = query.Include(item);

        return query;
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = Table.AsTracking();
        query = query.Where(predicate);

        if (includeProperties.Any())
            foreach (var item in includeProperties)
                query = query.Include(item);

        return await query.FirstOrDefaultAsync();
    }

    public T Get(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = Table.AsTracking();
        query = query.Where(predicate);

        if (includeProperties.Any())
            foreach (var item in includeProperties)
                query = query.Include(item);

        return query.FirstOrDefault();
    }
}