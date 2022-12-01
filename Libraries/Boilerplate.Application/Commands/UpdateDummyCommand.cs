using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Application.Commands;

public class UpdateDummyCommand
{
    [Required(ErrorMessage = "Id is required.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
}