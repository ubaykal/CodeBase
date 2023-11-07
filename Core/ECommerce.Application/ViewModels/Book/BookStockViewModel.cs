namespace ECommerce.Application.ViewModels.Book;

public class BookStockViewModel
{
    public string Name { get; set; }
    public int StockQuantity { get; set; }
    public decimal Price { get; set; }
    public Guid ProductId { get; set; }
}