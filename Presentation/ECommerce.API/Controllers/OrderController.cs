using ECommerce.Application.MediatR.Commands.Order;
using ECommerce.Application.MediatR.Queries.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerce.API.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        [SwaggerOperation(
            Summary = "Ürün sipariş metodu")]
        public async Task<IActionResult> Create(CreateOrderCommandRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPost]
        [Route("OrderDateFilter")]
        [SwaggerOperation(
            Summary = "Başlangıç bitiş tarihine göre order listesi döner")]
        public async Task<IActionResult> OrderDateFilter(OrderDateFilterQueryRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
        
        [HttpPost]
        [Route("CustomerOrderById")]
        [SwaggerOperation(
            Summary = "Ürün sipariş detay servisi")]
        public async Task<IActionResult> CustomerOrderById(CustomerOrderByIdQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}