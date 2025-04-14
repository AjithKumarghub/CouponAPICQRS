namespace CouponAPI.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<LocalUser> LocalUsers { get; set; }
}
