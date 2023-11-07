using ECommerce.Application.Abstraction;
using MediatR;

namespace ECommerce.Application.MediatR.Queries.Order;

public class
    CustomerOrderByIdQueryHandler : IRequestHandler<CustomerOrderByIdQueryRequest, CustomerOrderByIdQueryResponse>
{
    private readonly IOrderService _orderService;

    public CustomerOrderByIdQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<CustomerOrderByIdQueryResponse> Handle(CustomerOrderByIdQueryRequest request, CancellationToken
        cancellationToken)
    {
        return await _orderService.CustomerOrderById(request);
    }
}