using ECommerce.Application.MediatR.Commands;
using ECommerce.Application.MediatR.Queries.Customers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerce.API.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Create")]
        [SwaggerOperation(
            Summary = "Yeni müşteri oluşturma servisi")]
        public async Task<IActionResult> Create(CreateCustomerCommandRequest createUserCommandRequest)
        {
            var response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpPost]
        [Route("CustomerOrders")]
        [SwaggerOperation(
            Summary = "Sipriş oluşturma servisi")]
        public async Task<IActionResult> CustomerOrders(AllCustomerQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}