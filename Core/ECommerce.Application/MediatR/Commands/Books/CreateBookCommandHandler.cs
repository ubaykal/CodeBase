using ECommerce.Application.Abstraction;
using MediatR;

namespace ECommerce.Application.MediatR.Commands.Books;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommandRequest, CreateBookCommandResponse>
{
    private readonly IBookService _bookService;

    public CreateBookCommandHandler(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<CreateBookCommandResponse> Handle(CreateBookCommandRequest request, CancellationToken 
    cancellationToken)
    {
        var result = await _bookService.Create(request);

        return result;
    }
}