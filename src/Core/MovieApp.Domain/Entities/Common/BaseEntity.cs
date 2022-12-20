using System.ComponentModel.DataAnnotations;

namespace MovieApp.Domain.Entities;

public abstract class BaseEntity<T> : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public T Id { get; set; }
}


public class BaseEntity : IEntity
{

}

