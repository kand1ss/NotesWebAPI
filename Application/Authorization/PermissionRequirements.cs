using Microsoft.AspNetCore.Authorization;

namespace Application.Authorization;

public record PermissionRequirements(string Permission) : IAuthorizationRequirement;