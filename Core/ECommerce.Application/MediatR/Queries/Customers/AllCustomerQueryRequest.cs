using ECommerce.Application.ViewModels.UserViewmodels;
using MediatR;

namespace ECommerce.Application.MediatR.Queries.Customers;

public class AllCustomerQueryRequest : PageViewModel, IRequest<AllCustomerQueryResponse>
{

}