using ECommerce.Application.ViewModels.UserViewmodels;

namespace ECommerce.Application.MediatR.Queries.Customers;

public class AllCustomerQueryResponse
{
    public List<CustomerOrderViewModel> Customerorders { get; set; } = new();
    public int TotalNumberRegistrations { get; set; }
}