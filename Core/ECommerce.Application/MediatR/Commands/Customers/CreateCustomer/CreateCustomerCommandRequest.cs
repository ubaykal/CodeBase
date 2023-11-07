using MediatR;

namespace ECommerce.Application.MediatR.Commands;

public class CreateCustomerCommandRequest : IRequest<CreateCustomerCommandResponse>
{
    public string NameSurname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
}