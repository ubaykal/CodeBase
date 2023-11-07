namespace ECommerce.Domain.Entities.Common;

public class BaseEntity
{
    public Guid Id { get; set; }
    public Guid CreatedUser { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? UpdatedUser { get; set; }
    public DateTime? UpdatedDate { get; set; }
}