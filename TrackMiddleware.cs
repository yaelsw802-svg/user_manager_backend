public class TrackMiddleware
{
    private readonly RequestDelegate _next;

    public TrackMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        await _next(context);
    }
}