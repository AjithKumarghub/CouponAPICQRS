namespace CouponAPI.Configurations;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
    {
        // Register Swagger services
        services.AddOpenApiDocument(config =>
        {
            config.Title = "CouponAPI";
            config.Version = "v1";

            config.AddSecurity("Bearer", Array.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.Http,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Enter JWT token (without 'Bearer' prefix)"
            });

            config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
        });

        return services;
    }
}

