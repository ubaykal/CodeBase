using ECommerce.Application.ViewModels.Statistic;

namespace ECommerce.Application.MediatR.Queries.Statistic;

public class MonthlyOrderStatisticQueryResponse
{
    public string Month { get; set; }
    public int TotalOrderCount { get; set; }
    public int TotalBookCount { get; set; }
    public decimal TotalPurchasedAmoun { get; set; }
}