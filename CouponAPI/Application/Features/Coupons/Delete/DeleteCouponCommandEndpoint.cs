namespace CouponAPI.Application.Features.Coupons.Delete;

public static class DeleteCouponCommandEndpoint
{
    public static void MapDeleteCouponEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:int}", async (ISender mediator, int id) =>
        {
            var result = await mediator.Send(new DeleteCouponCommand(id));
            return Results.Json(result, statusCode: (int)result.StatusCode);
        })
        .RequireAuthorization()
        .WithName("DeleteCoupon")
        .Produces<APIResponse>(204)
        .Produces<APIResponse>(404)
        .Produces<APIResponse>(401);
    }
}

