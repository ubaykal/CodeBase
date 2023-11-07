using ECommerce.Application.Repositories.Orders;
using ECommerce.Persistence.Context;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Persistence.Repositories.Order;

public class OrderWriteRepository : WriteRepository<Domain.Entities.Order>, IOrderWriteRepository
{
    public OrderWriteRepository(ECommerceDbContext context, IHttpContextAccessor httpContextAccessor) : base
        (context, httpContextAccessor)
    {
        
    }
}