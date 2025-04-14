namespace CouponAPI.Infrastructure.Repository;

public class AuthRepository(ApplicationDbContext db) : IAuthRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<LocalUser?> GetByUsernameAsync(string username)
    {
        return await _db.LocalUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());
    }

    public async Task<bool> IsUniqueUserAsync(string username)
    {
        return !await _db.LocalUsers.AnyAsync(u => u.UserName == username);
    }

    public async Task<LocalUser> CreateAsync(LocalUser user)
    {
        _db.LocalUsers.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }
}

