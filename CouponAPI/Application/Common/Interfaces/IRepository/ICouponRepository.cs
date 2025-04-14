namespace CouponAPI.Application.Common.Interfaces.IRepository;

public interface ICouponRepository
{
    Task<ICollection<Coupon>> GetAllAsync();
    Task<Coupon?> GetAsync(int id);
    Task<Coupon?> GetAsync(string name);
    Task CreateAsync(Coupon coupon);
    Task UpdateAsync(Coupon coupon);
    Task DeleteAsync(Coupon coupon);
    Task<bool> ExistsByNameAsync(string name, int excludeId);
    Task<ICollection<Coupon>> GetFilterAsync(string couponName, int pageNumber = 1, int pageSize = 10);
}
