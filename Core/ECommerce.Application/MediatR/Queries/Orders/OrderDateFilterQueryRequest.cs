using ECommerce.Application.ViewModels.UserViewmodels;
using MediatR;

namespace ECommerce.Application.MediatR.Queries.Order;

public class OrderDateFilterQueryRequest : PageViewModel, IRequest<OrderDateFilterQueryResponse>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}