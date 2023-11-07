using ECommerce.Application.ViewModels.UserViewmodels;
using MediatR;

namespace ECommerce.Application.MediatR.Queries.Order;

public class CustomerOrderQueryRequest : PageViewModel, IRequest<CustomerOrderQueryResponse>
{
    public Guid CustomerId { get; set; }
}