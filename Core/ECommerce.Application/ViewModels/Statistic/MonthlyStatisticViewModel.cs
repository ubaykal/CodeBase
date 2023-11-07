namespace ECommerce.Application.ViewModels.Statistic;

public class MonthlyStatisticViewModel
{
    public string Month { get; set; }
    public int TotalOrderCount { get; set; }
    public int TotalBookCount { get; set; }
    public decimal TotalPurchasedAmoun { get; set; }
}