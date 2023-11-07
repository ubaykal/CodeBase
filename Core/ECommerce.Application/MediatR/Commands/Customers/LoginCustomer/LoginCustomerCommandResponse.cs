using ECommerce.Application.ViewModels.Token;

namespace ECommerce.Application.MediatR.Commands.Customers.LoginUser;

public class LoginCustomerCommandResponse
{
    public TokenViewModel Token { get; set; }
}