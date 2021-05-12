using GroceryStoreAPI.CustomMiddleware;
using Microsoft.AspNetCore.Builder;

namespace GroceryStoreAPI.Frameworks
{
	public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
