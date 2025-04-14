namespace CouponAPI.Application.Features.Coupons.Update;

public record UpdateCouponCommand(CouponUpdateDTO CouponUpdateDTO) : IRequest<APIResponse>;

public class UpdateCouponValidator : AbstractValidator<UpdateCouponCommand>
{
    public UpdateCouponValidator(ApplicationDbContext db)
    {
        RuleFor(c => c.CouponUpdateDTO.Name)
            .NotEmpty()
            .Must((cmd, name) => !db.Coupons.Any(x => x.Name.ToLower() == name.ToLower() && x.Id != cmd.CouponUpdateDTO.Id))
            .WithMessage("Coupon name must be unique.");

        RuleFor(c => c.CouponUpdateDTO.Percent)
            .InclusiveBetween(1, 100);
    }
}

public class UpdateCouponCommandHandler(ICouponRepository couponRepo)
    : IRequestHandler<UpdateCouponCommand, APIResponse>
{
    public async Task<APIResponse> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
    {
        var existing = await couponRepo.GetAsync(request.CouponUpdateDTO.Id);
        if (existing is null)
        {
            return new APIResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                IsSuccess = false,
                ErrorMessages = ["Coupon not found"]
            };
        }

        var coupon = request.CouponUpdateDTO.Adapt<Coupon>();
        await couponRepo.UpdateAsync(coupon);

        return new APIResponse
        {
            StatusCode = HttpStatusCode.OK,
            IsSuccess = true,
            Result = existing
        };
    }
}

