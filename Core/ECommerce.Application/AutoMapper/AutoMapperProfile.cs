using AutoMapper;
using ECommerce.Application.MediatR.Commands;
using ECommerce.Application.MediatR.Commands.Books;
using ECommerce.Application.MediatR.Commands.Users.RefreshTokenLogin;
using ECommerce.Application.MediatR.Queries.Books;
using ECommerce.Application.MediatR.Queries.Statistic;
using ECommerce.Application.ViewModels;
using ECommerce.Application.ViewModels.Book;
using ECommerce.Application.ViewModels.Statistic;
using ECommerce.Application.ViewModels.Token;
using ECommerce.Application.ViewModels.UserViewmodels;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateUserResponseModel, CreateCustomerCommandResponse>().ReverseMap();
        CreateMap<RefreshTokenLoginCommandResponse, TokenViewModel>().ReverseMap();
        CreateMap<BookStockControlQueryRequest, BookStockViewModel>().ReverseMap();
        CreateMap<CreateUserRequestModel, Customer>().ReverseMap();
        CreateMap<CreateCustomerCommandRequest, Customer>().ReverseMap();
        CreateMap<CustomerViewModel, Customer>().ReverseMap();
        CreateMap<CreateBookCommandRequest, Product>().ReverseMap();
        CreateMap<MonthlyStatisticViewModel, MonthlyOrderStatisticQueryResponse>().ReverseMap();
        CreateMap<BookStockControlQueryResponse, Product>().ReverseMap()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(s => s.Id));
    }
}