using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }

    [Required] public string Name { get; set; }

    [Required] public string Surname { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}