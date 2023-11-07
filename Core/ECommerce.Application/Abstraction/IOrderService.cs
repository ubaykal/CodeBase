using ECommerce.Application.MediatR.Commands.Order;
using ECommerce.Application.MediatR.Queries.Order;
using ECommerce.Application.ViewModels.UserViewmodels;

namespace ECommerce.Application.Abstraction;

public interface IOrderService
{
    CreateOrderCommandResponse CreateOrder(CreateOrderCommandRequest request);
    Task<CustomerOrderQueryResponse> CustomerOrders(CustomerOrderQueryRequest request);
    Task<OrderDateFilterQueryResponse> OrderDateFilter(OrderDateFilterQueryRequest request);
    Task<CustomerOrderByIdQueryResponse> CustomerOrderById(CustomerOrderByIdQueryRequest request);
    Task<List<CustomerOrderViewModel>> GetCustomerOrder(List<CustomerOrderViewModel> orders);
}