using CouponAPI.Application.Features.Coupons.Create;
using CouponAPI.Application.Features.Coupons.Delete;
using CouponAPI.Application.Features.Coupons.Get;
using CouponAPI.Application.Features.Coupons.Update;

namespace CouponAPI.Application.Features.Coupons;

public static class CouponEndpoints
{
    public static void MapCouponEndpoints(this RouteGroupBuilder group)
    {
        group.MapCreateCouponEndpoint();
        group.MapGetCouponEndpoint();
        group.MapUpdateCouponEndpoint();
        group.MapDeleteCouponEndpoint();
    }
}

