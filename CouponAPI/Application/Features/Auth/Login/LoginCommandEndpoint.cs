namespace CouponAPI.Application.Features.Auth.Login;

public static class LoginCommandEndpoint
{
    public static void MapLoginEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/login", async (ISender sender, LoginCommand command) =>
        {
            var result = await sender.Send(command);
            return Results.Json(result, statusCode: (int)result.StatusCode);
        })
        .WithName("Login")
        .Accepts<LoginCommand>("application/json")
        .Produces<APIResponse>(200)
        .Produces<APIResponse>(400);
    }
}

