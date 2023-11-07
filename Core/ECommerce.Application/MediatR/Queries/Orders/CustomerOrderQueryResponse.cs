using ECommerce.Application.ViewModels.UserViewmodels;

namespace ECommerce.Application.MediatR.Queries.Order;

public class CustomerOrderQueryResponse
{
   public List<CustomerOrderViewModel> CustomerOrders { get; set; }
   public int TotalNumberRegistrations { get; set; }
}