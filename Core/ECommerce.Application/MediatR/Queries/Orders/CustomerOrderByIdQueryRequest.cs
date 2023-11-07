using ECommerce.Application.ViewModels.UserViewmodels;
using MediatR;

namespace ECommerce.Application.MediatR.Queries.Order;

public class CustomerOrderByIdQueryRequest : PageViewModel, IRequest<CustomerOrderByIdQueryResponse>
{
    public Guid ProductId { get; set; }
}