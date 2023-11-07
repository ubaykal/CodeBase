using ECommerce.Application.Repositories.Products;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Context;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Persistence.Repositories.Products;

public class ProductWriteRepository : WriteRepository<Domain.Entities.Product>, IProductWriteRepository
{
    public ProductWriteRepository(ECommerceDbContext context, IHttpContextAccessor httpContextAccessor) : base
    (context, httpContextAccessor)
    {
    }
}