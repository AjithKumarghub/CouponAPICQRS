namespace CouponAPI.API;

public record APIResponse
{
    public bool IsSuccess { get; set; }
    public object? Result { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public List<string> ErrorMessages { get; set; } = [];
}
