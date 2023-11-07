using ECommerce.Application.Abstraction;
using MediatR;

namespace ECommerce.Application.MediatR.Queries.Statistic;

public class
    MonthlyOrderStatisticQueryHandler : IRequestHandler<MonthlyOrderStatisticQueryRequest,
        List<MonthlyOrderStatisticQueryResponse>>
{
    private readonly IStatisticService _statisticService;

    public MonthlyOrderStatisticQueryHandler(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }

    public Task<List<MonthlyOrderStatisticQueryResponse>> Handle(MonthlyOrderStatisticQueryRequest request,
        CancellationToken cancellationToken)
    {
        var result = _statisticService.MonthylOrderStatistic(request);

        return Task.FromResult(result);
    }
}