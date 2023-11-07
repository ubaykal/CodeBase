
using ECommerce.Application.MediatR.Commands.Books;
using ECommerce.Application.MediatR.Queries.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerce.API.Controllers
{
    public class BookController : BaseController
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("StockControl")]
        [SwaggerOperation(
            Summary = "Id ye göre kitap stok bilgisini döner")]
        public async Task<IActionResult> StockControl([FromQuery] BookStockControlQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        [SwaggerOperation(
            Summary = "Yeni kitap kaydı")]
        public async Task<IActionResult> Create(CreateBookCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("Update")]
        [SwaggerOperation(
            Summary = "Kitap stoğunu günceller")]
        public async Task<IActionResult> Update(UpdateBookCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}