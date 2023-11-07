using Microsoft.Extensions.Configuration;

namespace ECommerce.Persistence;

public class Connection
{
    public static string ConnectionStringDPostgreSql
    {
        get
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
                "../../Presentation/ECommerce.API"));
            configurationManager.AddJsonFile("appsettings.json");

            return configurationManager.GetConnectionString("PostgreSQL");
        }
    }
}