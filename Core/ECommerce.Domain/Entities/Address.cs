using Microsoft.EntityFrameworkCore;

namespace ECommerce.Domain.Entities;

[Owned]
public class Address
{
    public string District { get; set; }
    public string City { get; set; }
}