using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities;

public class Dummy : BaseEntity
{
    [Required] public string Name { get; set; }
}