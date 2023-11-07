using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.ViewModels.UserViewmodels;

public class CustomerOrderViewModel
{
    public string NameSurname { get; set; }
    public string ProductName { get; set; }
    public string? Description { get; set; }
    public int TotalCount { get; set; }
    public decimal TotalPrice { get; set; }
    public Address Address { get; set; } = new Address();
    public string OrderStatus { get; set; }
    public string PaymentStatus { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
}