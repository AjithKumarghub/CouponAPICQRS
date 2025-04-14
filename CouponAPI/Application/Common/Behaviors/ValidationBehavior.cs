namespace CouponAPI.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators.Select(v => v.Validate(context))
                                  .SelectMany(r => r.Errors)
                                  .Where(f => f != null)
                                  .ToList();

        if (failures.Any())
        {
            var response = new APIResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccess = false,
                ErrorMessages = failures.Select(f => f.ErrorMessage).ToList()
            };

            return (TResponse)(object)response!;
        }

        return await next();
    }
}


