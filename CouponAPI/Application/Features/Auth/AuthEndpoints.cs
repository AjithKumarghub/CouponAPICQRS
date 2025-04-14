using CouponAPI.Application.Features.Auth.Login;
using CouponAPI.Application.Features.Auth.Register;

namespace CouponAPI.Application.Features.Auth;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this RouteGroupBuilder group)
    {
        group.MapRegisterEndpoint();
        group.MapLoginEndpoint();
    }
}

