using ECommerce.Application.Repositories.Customers;
using ECommerce.Persistence.Context;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Persistence.Repositories.Customer;

public class CustomerWriteRepository: WriteRepository<Domain.Entities.Customer>, ICustomerWriteRepository
{
    public CustomerWriteRepository(ECommerceDbContext context, IHttpContextAccessor httpContextAccessor) : base
        (context, httpContextAccessor)
    {
        
    }
}