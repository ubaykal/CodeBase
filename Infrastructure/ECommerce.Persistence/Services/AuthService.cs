using ECommerce.Application.Abstraction;
using ECommerce.Application.Emuns;
using ECommerce.Application.Helpers;
using ECommerce.Application.Repositories.Customers;
using ECommerce.Application.ViewModels.BaseResponseModels;
using ECommerce.Application.ViewModels.Token;

namespace ECommerce.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly ITokenHandlerService _tokenHandlerService;
    private readonly ICustomerService _customerService;
    private readonly ICustomerReadRepository _customerReadRepository;

    public AuthService(ITokenHandlerService tokenHandlerService, ICustomerService customerService,
        ICustomerReadRepository customerReadRepository)
    {
        _tokenHandlerService = tokenHandlerService;
        _customerService = customerService;
        _customerReadRepository = customerReadRepository;
    }

    public async Task<TokenViewModel> LoginAsync(string usernameOrEmail, string password)
    {
        var customer =
            await _customerReadRepository.GetAsync(x => x.UserName == usernameOrEmail || x.Email == usernameOrEmail);

        if (customer == null)
            throw new ApiException(ErrorCode.NotFoundCustomer);
        
        if (password != customer.Password.PasswordDecrypt())
            throw new ApiException(ErrorCode.AuthenticationError);

        var token = _tokenHandlerService.CreateAccessToken(customer);
        // await _customerService.UpdateRefreshTokenAsync(token.RefreshToken, customer, token.Expiration);
        return token;
    }

    public async Task<TokenViewModel> RefreshTokenLoginAsync(string refreshToken)
    {
        var customer = await _customerReadRepository.GetAsync(x => x.RefreshToken == refreshToken);
        if (customer != null && customer?.RefreshTokenEndDate > DateTime.UtcNow)
        {
            var token = _tokenHandlerService.CreateAccessToken(customer);
            await _customerService.UpdateRefreshTokenAsync(token.RefreshToken, customer, token.Expiration);
            return token;
        }
        else
            throw new ApiException(ErrorCode.NotFoundCustomer);
    }
}