using MediatR;

namespace ECommerce.Application.MediatR.Queries.Statistic;

public class MonthlyOrderStatisticQueryRequest : IRequest<List<MonthlyOrderStatisticQueryResponse>>
{
    public Guid CustomerId { get; set; }
}