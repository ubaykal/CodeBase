using AutoMapper;
using ECommerce.Application.Abstraction;
using MediatR;

namespace ECommerce.Application.MediatR.Queries.Books;

public class BookStockControlQueryHandler : IRequestHandler<BookStockControlQueryRequest, BookStockControlQueryResponse>
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public BookStockControlQueryHandler(IBookService bookService, IMapper mapper)
    {
        _bookService = bookService;
        _mapper = mapper;
    }

    public async Task<BookStockControlQueryResponse> Handle(BookStockControlQueryRequest request, CancellationToken 
    cancellationToken)
    {
        return await _bookService.GetBookAsync(request);
    }
}