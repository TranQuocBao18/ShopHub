using Microsoft.AspNetCore.Identity;

namespace ShopHub.Modules.Identity.Domain.Entities;

public class ApplicationUserRole : IdentityUserRole<Guid>
{
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public Guid? AssignedBy { get; set; }
}
