using Microsoft.AspNetCore.Mvc;

internal sealed class GlobalExceptionMiddlware(
        RequestDelegate next,
        IProblemDetailsService problemDetailsService)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = context,
                Exception = ex,
                ProblemDetails = new ProblemDetails
                {
                    Type = ex.GetType().Name,
                    Title = "An error occured",
                    Detail = ex.Message
                }
            });
        }
    }
}
