using AutoMapper;
using ECommerce.Application.Abstraction;
using MediatR;

namespace ECommerce.Application.MediatR.Commands.Users.RefreshTokenLogin;

public class
    RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
{
    private readonly IAuthService _authService;
    private IMapper _mapper;

    public RefreshTokenLoginCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request,
        CancellationToken cancellationToken)
    {
        var token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);

        var result = _mapper.Map<RefreshTokenLoginCommandResponse>(token);

        return result;
    }
}