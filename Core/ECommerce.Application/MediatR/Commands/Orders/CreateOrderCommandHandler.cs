using ECommerce.Application.Abstraction;
using MediatR;

namespace ECommerce.Application.MediatR.Commands.Order;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
{
    private readonly IOrderService _orderService;


    public CreateOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request,
        CancellationToken cancellationToken)
    {
        var result = _orderService.CreateOrder(request);

        return Task.FromResult(result);
    }
}