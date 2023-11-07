using ECommerce.Application.ViewModels.Token;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Abstraction;

public interface ITokenHandlerService
{
    TokenViewModel  CreateAccessToken(Customer customer);
    string CreateRefreshToken();
}