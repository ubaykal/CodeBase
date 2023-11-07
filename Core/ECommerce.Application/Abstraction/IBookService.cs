using ECommerce.Application.MediatR.Commands.Books;
using ECommerce.Application.MediatR.Queries.Books;
using ECommerce.Application.ViewModels.Book;

namespace ECommerce.Application.Abstraction;

public interface IBookService
{
    Task<CreateBookCommandResponse> Create(CreateBookCommandRequest request);
    Task<bool> Update(UpdateBookCommandRequest request);
    Task<BookStockControlQueryResponse> GetBookAsync(BookStockControlQueryRequest request);
    BookStockViewModel GetBook(Guid id);
    bool BookStockCheck(Guid id, int quantity);
}