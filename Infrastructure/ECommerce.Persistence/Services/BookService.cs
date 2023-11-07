using AutoMapper;
using ECommerce.Application.Abstraction;
using ECommerce.Application.Emuns;
using ECommerce.Application.MediatR.Commands.Books;
using ECommerce.Application.MediatR.Queries.Books;
using ECommerce.Application.Repositories.Products;
using ECommerce.Application.ViewModels.BaseResponseModels;
using ECommerce.Application.ViewModels.Book;
using ECommerce.Domain.Entities;

namespace ECommerce.Persistence.Services;

public class BookService : IBookService
{
    private readonly IMapper _mapper;
    private readonly IProductReadRepository _productRead;
    private readonly IProductWriteRepository _productWrite;

    public BookService(IProductReadRepository productRead, IMapper mapper, IProductWriteRepository productWrite)
    {
        _productRead = productRead;
        _mapper = mapper;
        _productWrite = productWrite;
    }

    public async Task<CreateBookCommandResponse> Create(CreateBookCommandRequest request)
    {
        var result = await _productRead.GetAsync(x => x.Name == request.Name);

        if (result != null)
            throw new ApiException(ErrorCode.ExistBook);

        var product = _mapper.Map<Product>(request);
        _ = await _productWrite.AddAsync(product);
        await _productWrite.SaveAsync();
        return new CreateBookCommandResponse()
        {
            Id = product.Id
        };
    }

    public async Task<bool> Update(UpdateBookCommandRequest request)
    {
        if (request.StockQuantity <= 0)
            throw new ApiException(ErrorCode.StockControl);

        var book = await _productRead.GetAsync(x => x.Id == request.Id);

        if (book == null)
            throw new ApiException(ErrorCode.ExistBook);

        book.StockQuantity = request.StockQuantity;

        _productWrite.Update(book);
        return await _productWrite.SaveAsync() > 0;
    }

    public async Task<BookStockControlQueryResponse> GetBookAsync(BookStockControlQueryRequest request)
    {
        var product = await _productRead.GetAsync(x => x.Id == request.Id);

        if (product == null)
            throw new ApiException(ErrorCode.NotFound);

        var result = _mapper.Map<BookStockControlQueryResponse>(product);

        return result;
    }

    public BookStockViewModel GetBook(Guid id)
    {
        var product = _productRead.Get(x => x.Id == id);

        var result = new BookStockViewModel
        {
            ProductId = product.Id,
            Name = product.Name,
            StockQuantity = product.StockQuantity,
            Price = product.Price,
        };

        return result;
    }

    public bool BookStockCheck(Guid id, int quantity)
    {
        var product = _productRead.Get(x => x.Id == id && x.StockQuantity >= quantity);

        return product != null;
    }
}