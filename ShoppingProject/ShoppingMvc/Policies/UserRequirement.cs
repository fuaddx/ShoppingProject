using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ShoppingMvc.Contexts;

public class UserRequirement : IAuthorizationRequirement
{
}

public class UserHandler : AuthorizationHandler<UserRequirement>
{
    private readonly EvaraDbContext _db;

    public UserHandler(EvaraDbContext db)
    {
        _db = db;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Fail();
            return;
        }

        var userName = context.User.Identity.Name;

        var userData = await _db.Users.FirstOrDefaultAsync(x => x.UserName == userName);

        if (userData != null)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}