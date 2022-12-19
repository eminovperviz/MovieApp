namespace MovieApp.Domain.Entities;

public abstract class AuditableEntity<T> : BaseEntity<T>, IAuditableEntity
{
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public int? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}
