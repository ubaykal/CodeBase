using AutoMapper;
using ECommerce.Application.Abstraction;
using ECommerce.Application.Helpers;
using ECommerce.Application.MediatR.Queries.Statistic;
using ECommerce.Application.Repositories.Orders;
using ECommerce.Application.ViewModels.Statistic;

namespace ECommerce.Persistence.Services;

public class StatisticService : IStatisticService
{
    private readonly IMapper _mapper;
    private readonly IOrderItemReadRepository _orderItemReadRepository;

    public StatisticService(IOrderItemReadRepository orderItemReadRepository, IMapper mapper)
    {
        _orderItemReadRepository = orderItemReadRepository;
        _mapper = mapper;
    }

    public List<MonthlyOrderStatisticQueryResponse> MonthylOrderStatistic(
        MonthlyOrderStatisticQueryRequest request)
    {
        var monthlyStatistics = _orderItemReadRepository.GetAll()
            .GroupBy(x => new {x.CreatedDate.Year, x.CreatedDate.Month})
            .Select(s => new
            {
                Month = s.Key.Month,
                TotalOrderCount = s.Count(),
                TotalBookCount = s.Sum(o => o.TotalCount),
                TotalPurchasedAmount = s.Sum(o => o.TotalPrice)
            }).OrderBy(o => o.Month).ToList();

        var monthlyStatisticList = monthlyStatistics.Select(item => new MonthlyStatisticViewModel
        {
            Month = item.Month.GetMonthName(),
            TotalBookCount = item.TotalBookCount,
            TotalOrderCount = item.TotalOrderCount,
            TotalPurchasedAmoun = item.TotalPurchasedAmount
        }).ToList();

        var response = _mapper.Map<List<MonthlyOrderStatisticQueryResponse>>(monthlyStatisticList);
        return response;
    }
}