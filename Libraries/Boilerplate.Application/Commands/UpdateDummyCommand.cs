using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Application.Commands;

public class UpdateDummyCommand
{
    [Required(ErrorMessage = "Id is required.")]
    public int Id { get; private set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; private set; }

    public UpdateDummyCommand(int id, string name)
    {
        Id = id;
        Name = name;
    }
}