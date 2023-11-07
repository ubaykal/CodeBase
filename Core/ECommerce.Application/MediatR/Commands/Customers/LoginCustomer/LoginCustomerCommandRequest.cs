using MediatR;

namespace ECommerce.Application.MediatR.Commands.Customers.LoginUser;

public class LoginCustomerCommandRequest : IRequest<LoginCustomerCommandResponse>
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}