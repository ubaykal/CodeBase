using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ECommerce.Application.Abstraction;
using ECommerce.Application.Repositories.Customers;
using ECommerce.Application.Repositories.Orders;
using ECommerce.Application.Repositories.Products;
using ECommerce.Persistence.Context;
using ECommerce.Persistence.Repositories.Customer;
using ECommerce.Persistence.Repositories.Order;
using ECommerce.Persistence.Repositories.Orders;
using ECommerce.Persistence.Repositories.Products;
using ECommerce.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistentService(this IServiceCollection services)
    {
        services.AddDbContext<ECommerceDbContext>(option => option.UseNpgsql(Connection.ConnectionStringDPostgreSql));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddHttpContextAccessor();

        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IBookService, BookService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IStatisticService, StatisticService>();
        services.AddTransient<IStatisticService, StatisticService>();
        
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        services.AddScoped<IOrderItemWriteRepository, OrderItemWriteRepository>();
        services.AddScoped<IOrderItemReadRepository, OrderItemReadRepository>();
    }
}