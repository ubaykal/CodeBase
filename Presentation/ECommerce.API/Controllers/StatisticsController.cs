using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Application.MediatR.Commands.Order;
using ECommerce.Application.MediatR.Queries.Statistic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerce.API.Controllers
{
    public class StatisticsController : BaseController
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("MonthlyStatistic")]
        [SwaggerOperation(
            Summary = "Aylık istatistik servisi")]
        public async Task<IActionResult> MonthlyStatistic(MonthlyOrderStatisticQueryRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}
