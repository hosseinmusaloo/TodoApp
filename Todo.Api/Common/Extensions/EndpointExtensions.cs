namespace Todo.Api.Common.Extensions;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.MapEndpoint(app);
        return app;
    }
}