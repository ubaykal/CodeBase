using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities.Common;
using ECommerce.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerce.Persistence.Repositories;

public class WriteRepository<T> : IDisposable, IWriteRepository<T> where T : BaseEntity
{
    private readonly ECommerceDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public WriteRepository(ECommerceDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<bool> AddAsync(T entity)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedDate = DateTime.Now;

        entity.CreatedUser = entity is Domain.Entities.Customer ? Guid.Empty : GetUserId();
        
        var entityEntry = await Table.AddAsync(entity);

        return entityEntry.State == EntityState.Added;
    }

    public bool Add(T entity)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedDate = DateTime.Now;
        entity.CreatedUser = GetUserId();
            
        var entityEntry = Table.Add(entity);

        return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<T> datas)
    {
        await Table.AddRangeAsync(datas);
        return true;
    }

    public bool Update(T entity)
    {
        entity.UpdatedDate = DateTime.Now;
        entity.CreatedUser = GetUserId();
        
        EntityEntry entityEntry = Table.Update(entity);
        return entityEntry.State == EntityState.Modified;
    }

    public async Task<int> SaveAsync()
    {
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var affectedRow = await _context.SaveChangesAsync();
                transaction.Commit();

                return affectedRow;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }
    }

    public int Save()
    {
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var affectedRow = _context.SaveChanges();
                transaction.Commit();

                return affectedRow;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }
    }

    private Guid GetUserId()
    {
        return Guid.Parse(_httpContextAccessor.HttpContext.User.Claims?.FirstOrDefault(x => x.Type == "UserId")?.Value);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}