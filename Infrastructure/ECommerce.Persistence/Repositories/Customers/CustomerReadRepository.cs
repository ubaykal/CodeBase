using ECommerce.Application.Repositories.Customers;
using ECommerce.Persistence.Context;

namespace ECommerce.Persistence.Repositories.Customer;

public class CustomerReadRepository : ReadRepository<Domain.Entities.Customer>, ICustomerReadRepository
{
    public CustomerReadRepository(ECommerceDbContext context) : base(context)
    {
    }
}