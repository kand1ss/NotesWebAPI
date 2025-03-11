using System.Security.Claims;
using Core.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace Application.Authorization;

public class PermissionRequirementsHandler(IAccountPermissionsRepository repository) 
    : AuthorizationHandler<PermissionRequirements>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirements requirement)
    {
        var userGuid = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userGuid is null)
            context.Fail();

        var userPermissions = await repository.GetAccountPermissions(new Guid(userGuid));
        var permissionNames = userPermissions.Select(p => p.PermissionName).ToList();
        
        if(permissionNames.Contains(requirement.Permission))
            context.Succeed(requirement);
        else
            context.Fail();
    }
}