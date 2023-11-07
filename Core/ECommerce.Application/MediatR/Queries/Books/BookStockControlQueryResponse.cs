namespace ECommerce.Application.MediatR.Queries.Books;

public class BookStockControlQueryResponse
{
    public string Name { get; set; }
    public int StockQuantity { get; set; }
    public decimal Price { get; set; }
    public Guid ProductId { get; set; }
}