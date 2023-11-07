using ECommerce.Application.Abstraction;
using MediatR;

namespace ECommerce.Application.MediatR.Queries.Order;

public class CustomerOrderQueryHandler : IRequestHandler<CustomerOrderQueryRequest, CustomerOrderQueryResponse>
{
    private readonly IOrderService _orderService;

    public CustomerOrderQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<CustomerOrderQueryResponse> Handle(CustomerOrderQueryRequest request, CancellationToken
        cancellationToken)
    {
        return await _orderService.CustomerOrders(request);
    }
}