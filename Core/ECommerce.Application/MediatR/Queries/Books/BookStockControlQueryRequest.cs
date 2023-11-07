using MediatR;

namespace ECommerce.Application.MediatR.Queries.Books;

public class BookStockControlQueryRequest : IRequest<BookStockControlQueryResponse>
{
    public Guid Id { get; set; }
}