namespace CouponAPI.Application.Features.Coupons.Get;

public static class GetCouponQueryEndpoint
{
    public static void MapGetCouponEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:int}", async (ISender mediator, int id) =>
        {
            var result = await mediator.Send(new GetCouponQuery(id));
            return Results.Json(result, statusCode: (int)result.StatusCode);
        })
        .WithName("GetCoupon")
        .Produces<APIResponse>(200)
        .Produces<APIResponse>(404);
    }
}

