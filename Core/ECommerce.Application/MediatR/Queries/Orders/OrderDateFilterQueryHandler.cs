using ECommerce.Application.Abstraction;
using MediatR;

namespace ECommerce.Application.MediatR.Queries.Order;

public class OrderDateFilterQueryHandler : IRequestHandler<OrderDateFilterQueryRequest, OrderDateFilterQueryResponse>
{
    private readonly IOrderService _orderService;

    public OrderDateFilterQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<OrderDateFilterQueryResponse> Handle(OrderDateFilterQueryRequest request,
        CancellationToken cancellationToken)
    {
        return await _orderService.OrderDateFilter(request);
    }
}