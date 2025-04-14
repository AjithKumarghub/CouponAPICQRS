namespace CouponAPI.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException vex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var response = new APIResponse
            {
                IsSuccess = false,
                ErrorMessages = vex.Errors.Select(e => e.ErrorMessage).ToList(),
                StatusCode = HttpStatusCode.BadRequest
            };
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred.");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new APIResponse
            {
                IsSuccess = false,
                ErrorMessages = [ex.Message],
                StatusCode = HttpStatusCode.InternalServerError
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

