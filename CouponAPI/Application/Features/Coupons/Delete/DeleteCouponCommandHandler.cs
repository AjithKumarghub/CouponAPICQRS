namespace CouponAPI.Application.Features.Coupons.Delete;

public record DeleteCouponCommand(int Id) : IRequest<APIResponse>, IAuthorizableRequest
{
    public string[] Roles => ["Admin"]; // Only Admins can delete
}
public class DeleteCouponCommandHandler(ICouponRepository couponRepo)
    : IRequestHandler<DeleteCouponCommand, APIResponse>
{
    public async Task<APIResponse> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = await couponRepo.GetAsync(request.Id);
        if (coupon is null)
        {
            return new APIResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                IsSuccess = false,
                ErrorMessages = ["Coupon not found"]
            };
        }

        await couponRepo.DeleteAsync(coupon);

        return new APIResponse
        {
            StatusCode = HttpStatusCode.NoContent,
            IsSuccess = true
        };
    }
}

