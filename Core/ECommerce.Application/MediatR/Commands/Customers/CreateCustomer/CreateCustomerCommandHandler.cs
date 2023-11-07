using AutoMapper;
using ECommerce.Application.Abstraction;
using MediatR;

namespace ECommerce.Application.MediatR.Commands;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommandRequest, CreateCustomerCommandResponse>
{
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(ICustomerService customerService, IMapper mapper)
    {
        _customerService = customerService;
        _mapper = mapper;
    }

    public async Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommandRequest request, CancellationToken
        cancellationToken)
    {
        var response = await _customerService.CreateAsync(request);

        return _mapper.Map<CreateCustomerCommandResponse>(response);
    }
}