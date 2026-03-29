namespace ShopHub.Domain.Shared.Primitives;

public class AuditableBaseEntity : Entity<Guid>
{
    public virtual string? CreatedBy { get; set; }
    public virtual DateTime Created { get; set; }
    public virtual string? LastModifiedBy { get; set; }
    public virtual DateTime? LastModified { get; set; }
}
