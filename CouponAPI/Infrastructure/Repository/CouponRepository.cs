namespace CouponAPI.Infrastructure.Repository;

public class CouponRepository(ApplicationDbContext db) : ICouponRepository
{
    private readonly ApplicationDbContext _db = db;
    public async Task<ICollection<Coupon>> GetAllAsync()
    {
        return await _db.Coupons.AsNoTracking().ToListAsync();
    }

    public async Task<Coupon?> GetAsync(int id)
    {
        return await _db.Coupons.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Coupon?> GetAsync(string name)
    {
        return await _db.Coupons.AsNoTracking().FirstOrDefaultAsync(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
    }

    public async Task CreateAsync(Coupon coupon)
    {
        coupon.Created = DateTime.Now;
        await _db.Coupons.AddAsync(coupon);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Coupon coupon)
    {
        coupon.LastUpdated = DateTime.Now;
        _db.Coupons.Update(coupon);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Coupon coupon)
    {
        _db.Coupons.Remove(coupon);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name, int excludeId)
    {
        return await _db.Coupons.AnyAsync(x => x.Name == name && x.Id != excludeId);
    }

    public async Task<ICollection<Coupon>> GetFilterAsync(string couponName, int pageNumber = 1, int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(couponName))
        {
            return await _db.Coupons.Where(x => x.Name.Contains(couponName)).AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }
        return await _db.Coupons.AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
