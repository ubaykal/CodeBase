using ECommerce.Application.Repositories.Orders;
using ECommerce.Persistence.Context;

namespace ECommerce.Persistence.Repositories.Order;

public class OrderReadRepository : ReadRepository<Domain.Entities.Order>, IOrderReadRepository
{
    public OrderReadRepository(ECommerceDbContext context) : base(context)
    {
    }
}