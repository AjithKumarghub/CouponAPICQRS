namespace CouponAPI.Application.Common.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse>(
    IHttpContextAccessor httpContextAccessor)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not IAuthorizableRequest authRequest)
        {
            return await next(cancellationToken);
        }

        var httpContext = httpContextAccessor.HttpContext;
        var user = httpContext?.User;

        if (user?.Identity is not { IsAuthenticated: true })
        {
            return (TResponse)(object)new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.Unauthorized,
                ErrorMessages = ["Unauthorized"]
            };
        }

        var userRole = user.FindFirstValue(ClaimTypes.Role);
        if (userRole is null || !authRequest.Roles.Contains(userRole, StringComparer.OrdinalIgnoreCase))
        {
            return (TResponse)(object)new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.Forbidden,
                ErrorMessages = ["Forbidden: Insufficient role"]
            };
        }

        return await next(cancellationToken);
    }
}


