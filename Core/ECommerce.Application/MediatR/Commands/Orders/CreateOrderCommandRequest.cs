using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.MediatR.Commands.Order;

public class CreateOrderCommandRequest : IRequest<CreateOrderCommandResponse>
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Address Address { get; set; }
}