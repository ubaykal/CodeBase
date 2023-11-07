using MediatR;

namespace ECommerce.Application.MediatR.Commands.Books;

public class CreateBookCommandRequest : IRequest<CreateBookCommandResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}