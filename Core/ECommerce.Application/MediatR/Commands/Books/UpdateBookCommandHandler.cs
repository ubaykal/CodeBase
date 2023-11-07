using ECommerce.Application.Abstraction;
using MediatR;

namespace ECommerce.Application.MediatR.Commands.Books;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommandRequest, UpdateBookCommandResponse>
{
    private readonly IBookService _bookService;

    public UpdateBookCommandHandler(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<UpdateBookCommandResponse> Handle(UpdateBookCommandRequest request, CancellationToken
        cancellationToken)
    {
        _ = await _bookService.Update(request);

        return new UpdateBookCommandResponse()
        {
            Message = "Güncelleme başarılı"
        };
    }
}