using ECommerce.Application.Repositories.Orders;
using ECommerce.Persistence.Context;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Persistence.Repositories.Orders;

public class OrderItemWriteRepository: WriteRepository<Domain.Entities.OrderItem>, IOrderItemWriteRepository
{
    public OrderItemWriteRepository(ECommerceDbContext context, IHttpContextAccessor httpContextAccessor) : base
        (context, httpContextAccessor)
    {
        
    }
}