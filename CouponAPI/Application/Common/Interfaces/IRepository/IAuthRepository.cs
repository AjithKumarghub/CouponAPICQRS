namespace CouponAPI.Application.Common.Interfaces.IRepository;


public interface IAuthRepository
{
    Task<LocalUser?> GetByEmailAsync(string username);
    Task<bool> IsUniqueEmailAsync(string username);
    Task<LocalUser> CreateAsync(LocalUser user);
}

public interface IAuthorizableRequest
{
    string[] Roles { get; }
}

public interface IJwtTokenGenerator
{
    string GenerateToken(LocalUser user);
}

public interface IPasswordHasher
{
    void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt);
    bool VerifyPasswordHash(string password, string storedHash, string storedSalt);
}