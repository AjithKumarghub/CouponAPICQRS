namespace CouponAPI.Configurations;

public static class JwtAuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        // Register JwtTokenGenerator 
        var secret = config["ApiSettings:Secret"];
        if (string.IsNullOrWhiteSpace(secret))
            throw new InvalidOperationException("Missing JWT Secret in configuration.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = !config.GetValue<bool>("DisableHttpsMetadata"); // allow override for dev/test
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidIssuer = "CouponAPI",
                ValidAudience = "CouponAPIUsers",
                RoleClaimType = ClaimTypes.Role
            };
        });

        services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole("admin")); // Role-based auth

        return services;
    }
}

