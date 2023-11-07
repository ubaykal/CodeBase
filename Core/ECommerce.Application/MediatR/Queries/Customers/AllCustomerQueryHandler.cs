using ECommerce.Application.Abstraction;
using MediatR;

namespace ECommerce.Application.MediatR.Queries.Customers;

public class AllCustomerQueryHandler : IRequestHandler<AllCustomerQueryRequest, AllCustomerQueryResponse>
{
    private readonly ICustomerService _customerService;

    public AllCustomerQueryHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<AllCustomerQueryResponse> Handle(AllCustomerQueryRequest request, CancellationToken 
    cancellationToken)
    {
        return await _customerService.CustomerOrders(request);
    }
}