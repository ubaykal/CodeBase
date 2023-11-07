using ECommerce.Domain.Entities.Common;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities;

public class Order : BaseEntity
{
    public Guid Id { get; set; }
    public Guid BuyerId { get; set; }
    public Address Address { get; set; }
    public ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();
    
    public OrderStatus OrderStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}