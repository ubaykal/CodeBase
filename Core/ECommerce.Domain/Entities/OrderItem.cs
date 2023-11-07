using ECommerce.Domain.Entities.Common;

namespace ECommerce.Domain.Entities;

public class OrderItem : BaseEntity
{
    public int TotalCount { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public Guid ProductId { get; set; }
}