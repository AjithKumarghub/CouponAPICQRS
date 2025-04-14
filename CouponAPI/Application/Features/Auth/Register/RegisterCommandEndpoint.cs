using CouponAPI.Application.Features.Auth.Login;

namespace CouponAPI.Application.Features.Auth.Register;

public static class RegisterCommandEndpoint
{
    public static void MapRegisterEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/register", async (ISender sender, RegisterCommand command) =>
        {
            var result = await sender.Send(command);
            return Results.Json(result, statusCode: (int)result.StatusCode);
        })
        .WithName("Register")
        .Accepts<LoginCommand>("application/json")
        .Produces<APIResponse>(200)
        .Produces<APIResponse>(400);
    }
}
