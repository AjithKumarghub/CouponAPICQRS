using CouponAPI.Infrastructure.Services;

namespace CouponAPI.Configurations;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DB Connection
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Register Repositories
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ICouponRepository, CouponRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();

        return services;
    }
}
