using ECommerce.Application.Abstraction;
using MediatR;

namespace ECommerce.Application.MediatR.Commands.Customers.LoginUser;

public class LoginCustomerCommandHandler: IRequestHandler<LoginCustomerCommandRequest, LoginCustomerCommandResponse>
{
    readonly IAuthService _authService;
    public LoginCustomerCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginCustomerCommandResponse> Handle(LoginCustomerCommandRequest request, CancellationToken cancellationToken)
    {
        var token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password);
        return new LoginCustomerCommandResponse()
        {
            Token = token
        };
    }
}