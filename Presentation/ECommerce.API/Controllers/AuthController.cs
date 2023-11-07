using ECommerce.Application.MediatR.Commands.Customers.LoginUser;
using ECommerce.Application.MediatR.Commands.Users.RefreshTokenLogin;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerce.API.Controllers
{
    [Route("codebase/api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Login")]
        [SwaggerOperation(
            Summary = "Token alma servisi")]
        public async Task<IActionResult> Login(LoginCustomerCommandRequest loginUserCommandRequest)
        {
            var response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }

        [HttpPost]
        [Route("RefreshTokenLogin")]
        [SwaggerOperation(
            Summary = "Refresh token lma servisi")]
        public async Task<IActionResult> RefreshTokenLogin(
            [FromBody] RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
        {
            var response = await _mediator.Send(refreshTokenLoginCommandRequest);
            return Ok(response);
        }
    }
}