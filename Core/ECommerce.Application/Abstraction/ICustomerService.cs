using ECommerce.Application.MediatR.Commands;
using ECommerce.Application.MediatR.Queries;
using ECommerce.Application.MediatR.Queries.Customers;
using ECommerce.Application.ViewModels;
using ECommerce.Application.ViewModels.UserViewmodels;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Abstraction;

public interface ICustomerService
{
    Task<CreateUserResponseModel> CreateAsync(CreateCustomerCommandRequest request);
    Task UpdateRefreshTokenAsync(string refreshToken, Customer customer, DateTime accessTokenDate);
    Task<CustomerViewModel> GetCustomerAsync(string refreshToken);
    Task<AllCustomerQueryResponse> CustomerOrders(AllCustomerQueryRequest request);
}