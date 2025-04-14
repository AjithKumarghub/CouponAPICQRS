namespace CouponAPI.Configurations;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();

        // Register MediatR Handlers (CQRS)
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(currentAssembly));

        // Register FluentValidation Validators
        services.AddValidatorsFromAssembly(currentAssembly);



        // Register Cross-Cutting Conserns Behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

        return services;
    }
}
