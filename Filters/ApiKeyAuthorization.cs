using Microsoft.AspNetCore.Mvc.Filters;

namespace Worktastic.Filters;

public class ApiKeyAuthorization : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        throw new NotImplementedException();
    }
}