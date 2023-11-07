using ECommerce.Application.Repositories.Products;
using ECommerce.Persistence.Context;

namespace ECommerce.Persistence.Repositories.Products;

public class ProductReadRepository : ReadRepository<Domain.Entities.Product>, IProductReadRepository
{
    public ProductReadRepository(ECommerceDbContext context) : base(context)
    {
        
    }
}