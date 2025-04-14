namespace CouponAPI.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    public void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
    {
        var hmac = new HMACSHA512();
        passwordSalt = Convert.ToBase64String(hmac.Key);
        var pbkdf2 = new Rfc2898DeriveBytes(password, hmac.Key, 10000, HashAlgorithmName.SHA256);
        passwordHash = Convert.ToBase64String(pbkdf2.GetBytes(32));
    }

    public bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
    {
        var saltBytes = Convert.FromBase64String(storedSalt);
        var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256);
        var computedHash = Convert.ToBase64String(pbkdf2.GetBytes(32));
        return computedHash == storedHash;
    }
}

