namespace CouponAPI.Application.Features.Auth.Login;

public record LoginCommand(string Email, string Password) : IRequest<APIResponse>;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}

public class LoginCommandHandler(
    IAuthRepository authRepo,
    IPasswordHasher hasher,
    IJwtTokenGenerator jwtGenerator) : IRequestHandler<LoginCommand, APIResponse>
{
    public async Task<APIResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await authRepo.GetByEmailAsync(request.Email);
        if (user is null || !hasher.VerifyPasswordHash(request.Password, user.Password, user.PasswordSalt))
        {
            return new APIResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccess = false,
                ErrorMessages = ["Email or password is incorrect"]
            };
        }

        var token = jwtGenerator.GenerateToken(user);

        return new APIResponse
        {
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK,
            Result = new LoginResponseDTO(user.Adapt<UserDTO>(), token)
        };
    }
}



