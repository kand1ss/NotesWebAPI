namespace Application.Exceptions;

public class PermissionNotFoundException : Exception
{
    public PermissionNotFoundException(string permissionName) : base($"Permission with name \"{permissionName}\" was not found")
    {
    }

    public PermissionNotFoundException(int permissionId) : base($"Permission with ID {permissionId} does not exist")
    {
    }
}