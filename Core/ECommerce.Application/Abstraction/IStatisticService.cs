using ECommerce.Application.MediatR.Queries.Statistic;

namespace ECommerce.Application.Abstraction;

public interface IStatisticService
{
    List<MonthlyOrderStatisticQueryResponse> MonthylOrderStatistic(MonthlyOrderStatisticQueryRequest request);
}