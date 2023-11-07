using ECommerce.Application.Repositories.Orders;
using ECommerce.Persistence.Context;

namespace ECommerce.Persistence.Repositories.Order;

public class OrderItemReadRepository : ReadRepository<Domain.Entities.OrderItem>, IOrderItemReadRepository
{
    public OrderItemReadRepository(ECommerceDbContext context) : base(context)
    {
    }
}