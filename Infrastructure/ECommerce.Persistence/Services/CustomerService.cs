using AutoMapper;
using ECommerce.Application.Abstraction;
using ECommerce.Application.Emuns;
using ECommerce.Application.Helpers;
using ECommerce.Application.MediatR.Commands;
using ECommerce.Application.MediatR.Queries;
using ECommerce.Application.MediatR.Queries.Customers;
using ECommerce.Application.Repositories.Customers;
using ECommerce.Application.Repositories.Orders;
using ECommerce.Application.Repositories.Products;
using ECommerce.Application.ViewModels;
using ECommerce.Application.ViewModels.BaseResponseModels;
using ECommerce.Application.ViewModels.UserViewmodels;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Services;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly ICustomerWriteRepository _customerWriteRepository;
    private readonly IOrderItemReadRepository _orderItemReadRepository;
    private readonly IOrderService _orderService;

    public CustomerService(ICustomerWriteRepository customerWriteRepository,
        ICustomerReadRepository customerReadRepository, IMapper mapper,
        IOrderItemReadRepository orderItemReadRepository, IOrderService orderService)
    {
        _customerWriteRepository = customerWriteRepository;
        _customerReadRepository = customerReadRepository;
        _mapper = mapper;
        _orderItemReadRepository = orderItemReadRepository;
        _orderService = orderService;
    }

    public async Task<CreateUserResponseModel> CreateAsync(CreateCustomerCommandRequest request)
    {
        if (request.Password != request.PasswordConfirm)
            throw new ApiException(ErrorCode.PasswordConfirm);

        var userControl = await _customerReadRepository.GetAsync(x => x.UserName == request.UserName || x
                .Email ==
            request.Email);

        if (userControl != null)
            throw new ApiException(ErrorCode.ExistCustomer);

        var newCustomer = _mapper.Map<Customer>(request);
        newCustomer.Password = CodebaseHelpers.PasswordEncypt(request.Password);
        await _customerWriteRepository.AddAsync(newCustomer);
        await _customerWriteRepository.SaveAsync();
        return new CreateUserResponseModel
        {
            UserName = request.UserName
        };
    }

    public async Task UpdateRefreshTokenAsync(string refreshToken, Customer customer, DateTime accessTokenDate)
    {
        if (customer != null)
        {
            customer.RefreshToken = refreshToken;
            customer.RefreshTokenEndDate = accessTokenDate.AddMinutes(CodebaseHelpers.RefreshTokenExpireTime);
            
            _customerWriteRepository.Update(customer);
            await _customerWriteRepository.SaveAsync();
        }
        else
            throw new ApiException(ErrorCode.UnexpectedError);
    }

    public async Task<CustomerViewModel> GetCustomerAsync(string refreshToken)
    {
        var result = await _customerReadRepository.GetAsync(x => x.RefreshToken == refreshToken);
        var customer = _mapper.Map<CustomerViewModel>(result);

        return customer;
    }

    public async Task<AllCustomerQueryResponse> CustomerOrders(AllCustomerQueryRequest request)
    {
        var totalCount = await _orderItemReadRepository.Table.CountAsync();
        var orders = _orderItemReadRepository
            .GetAll(x => true, s => s.Order).Skip(request.PageIndex * request.ViewCount).Take
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

        orders = await _orderService.GetCustomerOrder(orders);

        return new AllCustomerQueryResponse
        {
            Customerorders = orders,
            TotalNumberRegistrations = totalCount
        };
    }
}