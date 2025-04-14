namespace CouponAPI.Infrastructure.Repository;

public class AuthRepository(ApplicationDbContext db) : IAuthRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<LocalUser?> GetByEmailAsync(string email)
    {
        return await _db.LocalUsers.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
    }

    public async Task<bool> IsUniqueEmailAsync(string email)
    {
        return !await _db.LocalUsers.AnyAsync(x => x.Email == email);
    }

    public async Task<LocalUser> CreateAsync(LocalUser user)
    {
        _db.LocalUsers.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }
}

