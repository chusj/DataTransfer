using Microsoft.AspNetCore.Authorization;

namespace Relay.Extension
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public List<PermissionItem> Permissions { get; set; } = new List<PermissionItem>();
    }
}
