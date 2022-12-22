using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities;

public class Role : BaseEntity
{
    [Required] public string Name { get; set; }
}