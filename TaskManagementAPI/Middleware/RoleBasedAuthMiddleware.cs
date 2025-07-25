using TaskManagementAPI.Services;

public class RoleBasedAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IUserService _userService;

    public RoleBasedAuthMiddleware(RequestDelegate next, IUserService userService)
    {
        _next = next;
        _userService = userService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip authentication for swagger
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }

        // Get user ID from header
        if (!context.Request.Headers.TryGetValue("X-User-Id", out var userIdHeader) ||
            !int.TryParse(userIdHeader, out int userId))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("User ID required");
            return;
        }

        // Get user and attach to context
        var user = await _userService.GetUserById(userId);
        if (user == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid user ID");
            return;
        }

        context.Items["User"] = user;
        await _next(context);
    }
}