namespace CouponAPI.Application.Features.Coupons.Create;

public record CreateCouponCommand(CouponCreateDTO CouponCreateDTO) : IRequest<APIResponse>;

public class CreateCouponValidator : AbstractValidator<CreateCouponCommand>
{
    public CreateCouponValidator(ApplicationDbContext db)
    {
        RuleFor(c => c.CouponCreateDTO.Name)
            .NotEmpty().WithMessage("Name is required")
            .Must(name => !db.Coupons.Any(x => x.Name.ToLower() == name.ToLower()))
            .WithMessage("Coupon name already exists");

        RuleFor(c => c.CouponCreateDTO.Percent)
            .InclusiveBetween(1, 100).WithMessage("Percent must be between 1 and 100.");
    }
}

public class CreateCouponCommandHandler(ICouponRepository couponRepo)
    : IRequestHandler<CreateCouponCommand, APIResponse>
{
    public async Task<APIResponse> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = request.CouponCreateDTO.Adapt<Coupon>();
        await couponRepo.CreateAsync(coupon);

        return new APIResponse
        {
            Result = coupon,
            IsSuccess = true,
            StatusCode = HttpStatusCode.Created
        };
    }
}


