using MediatR;

namespace ECommerce.Application.MediatR.Commands.Books;

public class UpdateBookCommandRequest : IRequest<UpdateBookCommandResponse>
{
    public Guid Id { get; set; }
    public int StockQuantity { get; set; }
}