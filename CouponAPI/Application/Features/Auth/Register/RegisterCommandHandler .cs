namespace CouponAPI.Application.Features.Auth.Register;

public record RegisterCommand(string Email, string Name, string Password) : IRequest<APIResponse>;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}

public class RegisterCommandHandler(
    IAuthRepository authRepo,
    IPasswordHasher hasher) : IRequestHandler<RegisterCommand, APIResponse>
{
    public async Task<APIResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var exists = await authRepo.IsUniqueEmailAsync(request.Email);
        if (!exists)
        {
            return new APIResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccess = false,
                ErrorMessages = ["Email already exists"]
            };
        }

        hasher.CreatePasswordHash(request.Password, out var hash, out var salt);
        var user = new LocalUser
        {
            Email = request.Email,
            Name = request.Name,
            Password = hash,
            PasswordSalt = salt,
            Role = "customer"
        };

        var created = await authRepo.CreateAsync(user);

        return new APIResponse
        {
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK,
            Result = created.Adapt<UserDTO>()
        };
    }
}


