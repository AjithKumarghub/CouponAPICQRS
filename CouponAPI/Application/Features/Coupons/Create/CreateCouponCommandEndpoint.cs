namespace CouponAPI.Application.Features.Coupons.Create;

public static class CreateCouponCommandEndpoint
{
    public static void MapCreateCouponEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (ISender mediator, CouponCreateDTO dto) =>
        {
            var command = new CreateCouponCommand(dto);
            var response = await mediator.Send(command);
            return Results.Json(response, statusCode: (int)response.StatusCode);
        })
        .RequireAuthorization()
        .WithName("CreateCoupon")
        .Accepts<CouponCreateDTO>("application/json")
        .Produces<APIResponse>(201)
        .Produces<APIResponse>(400);
    }
}

