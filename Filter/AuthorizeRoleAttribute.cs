using System.Security.Claims;
using blog.Helper;
using blog.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace blog.Filter
{
    public class AuthorizeRoleAttribute(RoleType requiredRole): AuthorizeAttribute, IAsyncAuthorizationFilter
    {
         public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.FindFirstValue("Id");
            // Console.WriteLine($"User ID----------->: {userId}");
            if (!Guid.TryParse(userId, out var userGuidId))
            {
                context.Result = new ObjectResult(new Response<string>
                {
                    Data = "",
                    Success = false,
                    Message = "Bạn không có quyền truy cập tài nguyên này.",
                    Status = 401
                })
                { StatusCode = 200 };
                return;
            }

            var userService = context.HttpContext.RequestServices.GetRequiredService<IUser>();

            var user = await userService.GetUserById(userGuidId);

            // Console.WriteLine($" user.Role: {user.Role}");

            // Nếu là role admin thì cho qua
            if (user == null || (user.Role != requiredRole && user.Role != RoleType.ADMIN))
            {
                context.Result = new ObjectResult(new Response<string>
                {
                    Data = "",
                    Success = false,
                    Message = "Bạn không có quyền truy cập tài nguyên này.",
                    Status = 401
                })
                { StatusCode = 200 };
                return;
            }
        }
    }
}