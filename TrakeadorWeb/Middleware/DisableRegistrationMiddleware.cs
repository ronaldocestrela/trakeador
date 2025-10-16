namespace TrakeadorWeb.Middleware
{
    public class DisableRegistrationMiddleware
    {
        private readonly RequestDelegate _next;

        public DisableRegistrationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Bloquear acesso às páginas de registro
            if (context.Request.Path.StartsWithSegments("/Identity/Account/Register"))
            {
                context.Response.Redirect("/Identity/Account/Login");
                return;
            }

            await _next(context);
        }
    }
}