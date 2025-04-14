using CouponAPI.Application.Features.Coupons.Create;

namespace CouponAPI.Application.Features.Coupons.Update;

public static class UpdateCouponCommandEndpoint
{
    public static void MapUpdateCouponEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/", async (ISender mediator, CouponUpdateDTO dto) =>
        {
            var command = new UpdateCouponCommand(dto);
            var response = await mediator.Send(command);
            return Results.Json(response, statusCode: (int)response.StatusCode);
        })
        .RequireAuthorization()
        .WithName("UpdateCoupon")
        .Accepts<CouponUpdateDTO>("application/json")
        .Produces<APIResponse>(200)
        .Produces<APIResponse>(400)
        .Produces<APIResponse>(404);
    }
}

