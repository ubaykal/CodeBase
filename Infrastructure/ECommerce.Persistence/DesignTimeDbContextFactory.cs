using ECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommerce.Persistence;

public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<ECommerceDbContext>
{
    public ECommerceDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ECommerceDbContext>();
        optionsBuilder.UseNpgsql(Connection.ConnectionStringDPostgreSql);

        return new ECommerceDbContext(optionsBuilder.Options);
    }
}