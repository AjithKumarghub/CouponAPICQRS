namespace CouponAPI.Application.Features.Coupons.Get;

public record GetCouponQuery(int Id) : IRequest<APIResponse>;
public class GetCouponQueryHandler(ICouponRepository couponRepo)
    : IRequestHandler<GetCouponQuery, APIResponse>
{
    public async Task<APIResponse> Handle(GetCouponQuery request, CancellationToken cancellationToken)
    {
        var coupon = await couponRepo.GetAsync(request.Id);

        return coupon is null
            ? new APIResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                IsSuccess = false,
                ErrorMessages = ["Coupon not found"]
            }
            : new APIResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = coupon
            };
    }
}

