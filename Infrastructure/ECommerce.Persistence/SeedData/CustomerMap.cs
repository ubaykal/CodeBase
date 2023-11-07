using ECommerce.Application.Helpers;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.SeedData;

public class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        var customer = new Customer()
        {
            Id = Guid.Parse("8408db88-09b8-4efe-ad1a-3a24d292f2dd"),
            NameSurname = "Utku Baykal",
            CreatedDate = DateTime.Now,
            UserName = "UBaykal",
            Email = "ubaykal@codebase.com",
            PhoneNumber = "05555555555,",
            Password = CodebaseHelpers.PasswordEncypt("1234")
        };

        builder.HasData(customer);
    }
}