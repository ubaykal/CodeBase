using ECommerce.Application.Abstraction;
using ECommerce.Application.Emuns;
using ECommerce.Application.Helpers;
using ECommerce.Application.MediatR.Commands.Order;
using ECommerce.Application.MediatR.Queries.Order;
using ECommerce.Application.Repositories.Customers;
using ECommerce.Application.Repositories.Orders;
using ECommerce.Application.Repositories.Products;
using ECommerce.Application.ViewModels.BaseResponseModels;
using ECommerce.Application.ViewModels.UserViewmodels;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Services;

public class OrderService : IOrderService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IOrderItemReadRepository _orderItemReadRepository;
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly IBookService _bookService;

    public OrderService(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository,
        IProductReadRepository productReadRepository, IBookService bookService,
        IProductWriteRepository productWriteRepository, IHttpContextAccessor httpContextAccessor,
        IOrderItemReadRepository orderItemReadRepository, ICustomerReadRepository customerReadRepository)
    {
        _orderReadRepository = orderReadRepository;
        _orderWriteRepository = orderWriteRepository;
        _productReadRepository = productReadRepository;
        _bookService = bookService;
        _productWriteRepository = productWriteRepository;
        _httpContextAccessor = httpContextAccessor;
        _orderItemReadRepository = orderItemReadRepository;
        _customerReadRepository = customerReadRepository;
    }

    public CreateOrderCommandResponse CreateOrder(CreateOrderCommandRequest request)
    {
        lock (new object())
        {
            if (!_bookService.BookStockCheck(request.ProductId, request.Quantity))
                throw new ApiException(ErrorCode.InsufficientStock);

            var orderItems = new List<OrderItem>();
            var book = _bookService.GetBook(request.ProductId);
            var orderItem = new OrderItem
            {
                ProductId = book.ProductId,
                TotalCount = request.Quantity,
                TotalPrice = request.Quantity * book.Price,
            };

            orderItems.Add(orderItem);
            var order = new Order
            {
                BuyerId = GetBuyerId(),
                Address = new Address
                {
                    District = request.Address.District,
                    City = request.Address.City,
                },
                OrderItem = orderItems,
                OrderStatus = OrderStatus.Inprogress,
                PaymentStatus = PaymentStatus.Pending
            };

            _orderWriteRepository.Add(order);
            _orderWriteRepository.Save();

            Payment(request, order.Id);

            return new CreateOrderCommandResponse()
            {
                Id = order.Id
            };
        }
    }

    public async Task<CustomerOrderQueryResponse> CustomerOrders(CustomerOrderQueryRequest request)
    {
        var orderList = await _orderReadRepository.GetAll(x => x.BuyerId == request.CustomerId)
            .Skip(request.PageIndex * request.ViewCount).Take(request.ViewCount).Select(s=> s.Id).ToListAsync();

        var totalCount = await _orderItemReadRepository.Table.CountAsync();
        
        var orders = _orderItemReadRepository
            .GetAll(x => orderList.Contains(x.OrderId), s => s.Order).Skip(request.PageIndex * request.ViewCount).Take
            (request
                .ViewCount).Select(s => new CustomerOrderViewModel()
            {
                OrderStatus = s.Order.OrderStatus.GetEnumDescription(),
                PaymentStatus = s.Order.PaymentStatus.GetEnumDescription(),
                Address = new Address()
                {
                    District = s.Order.Address.District,
                    City = s.Order.Address.City
                },
                TotalCount = s.TotalCount,
                TotalPrice = s.TotalPrice,
                CustomerId = s.Order.BuyerId,
                ProductId = s.ProductId,
            }).ToList();

        orders = await GetCustomerOrder(orders);
        return new CustomerOrderQueryResponse
        {
            CustomerOrders = orders,
            TotalNumberRegistrations = totalCount
        };
    }

    public async Task<OrderDateFilterQueryResponse> OrderDateFilter(OrderDateFilterQueryRequest request)
    {
        var totalCount = await _orderItemReadRepository.Table.CountAsync();
        var orders = _orderItemReadRepository
            .GetAll(x => x.CreatedDate >= request.StartDate && x.CreatedDate <=
                request.EndDate, s => s.Order).Skip(request.PageIndex * request.ViewCount).Take
            (request
                .ViewCount).Select(s => new CustomerOrderViewModel()
            {
                OrderStatus = s.Order.OrderStatus.GetEnumDescription(),
                PaymentStatus = s.Order.PaymentStatus.GetEnumDescription(),
                Address = new Address()
                {
                    District = s.Order.Address.District,
                    City = s.Order.Address.City
                },
                TotalCount = s.TotalCount,
                TotalPrice = s.TotalPrice,
                CustomerId = s.Order.BuyerId,
                ProductId = s.ProductId,
            }).ToList();

        orders = await GetCustomerOrder(orders);
        return new OrderDateFilterQueryResponse
        {
            Orders = orders,
            TotalNumberRegistrations = totalCount
        };
    }

    public async Task<CustomerOrderByIdQueryResponse> CustomerOrderById(CustomerOrderByIdQueryRequest request)
    {
        var totalCount = await _orderItemReadRepository.Table.CountAsync();
        var orders = _orderItemReadRepository
            .GetAll(x => x.ProductId == request.ProductId, s => s.Order).Skip(request.PageIndex * request.ViewCount).Take
            (request
                .ViewCount).Select(s => new CustomerOrderViewModel()
            {
                OrderStatus = s.Order.OrderStatus.GetEnumDescription(),
                PaymentStatus = s.Order.PaymentStatus.GetEnumDescription(),
                Address = new Address()
                {
                    District = s.Order.Address.District,
                    City = s.Order.Address.City
                },
                TotalCount = s.TotalCount,
                TotalPrice = s.TotalPrice,
                CustomerId = s.Order.BuyerId,
                ProductId = s.ProductId,
            }).ToList();

        orders = await GetCustomerOrder(orders);
        return new CustomerOrderByIdQueryResponse()
        {
            Orders = orders,
            TotalNumberRegistrations = totalCount
        };
    }

    public async Task<List<CustomerOrderViewModel>> GetCustomerOrder(List<CustomerOrderViewModel> orders)
    {
        var customerIds = orders.Select(s => s.CustomerId).ToList();
        var productIds = orders.Select(s => s.ProductId).ToList();

        var customers = await _customerReadRepository.GetAll(x => customerIds.Contains(x.Id)).Select(s => new
        {
            Id = s.Id,
            Name = s.NameSurname
        }).ToListAsync();

        var products = await _productReadRepository.GetAll(x => productIds.Contains(x.Id)).Select(s => new
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description
        }).ToListAsync();

        foreach (var item in orders)
        {
            item.NameSurname = customers.FirstOrDefault(x => x.Id == item.CustomerId)?.Name;
            item.ProductName = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            item.Description = products.FirstOrDefault(x => x.Id == item.ProductId)?.Description;
        }

        return orders;
    }

    private void Payment(CreateOrderCommandRequest request, Guid orderId)
    {
        var product = _productReadRepository.Get(x => x.Id == request.ProductId);

        product.StockQuantity -= request.Quantity;
        _productWriteRepository.Update(product);
        _productWriteRepository.Save();

        var order = _orderReadRepository.Get(x => x.Id == orderId);
        order.OrderStatus = OrderStatus.Complete;
        order.PaymentStatus = PaymentStatus.Success;

        _orderWriteRepository.Update(order);
        _orderWriteRepository.Save();
    }

    private Guid GetBuyerId()
    {
        return Guid.Parse(_httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "UserId")
            ?.Value);
    }
}