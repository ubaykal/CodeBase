using ECommerce.Application.ViewModels.UserViewmodels;

namespace ECommerce.Application.MediatR.Queries.Order;

public class CustomerOrderByIdQueryResponse
{
    public List<CustomerOrderViewModel> Orders { get; set; }
    public int TotalNumberRegistrations { get; set; }
}