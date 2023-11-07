using ECommerce.Application.ViewModels.Token;

namespace ECommerce.Application.Abstraction;

public interface IAuthService
{
    Task<TokenViewModel> LoginAsync(string usernameOrEmail, string password);
    Task<TokenViewModel> RefreshTokenLoginAsync(string refreshToken);
}