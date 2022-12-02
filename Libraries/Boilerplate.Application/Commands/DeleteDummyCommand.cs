using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Application.Commands;

public class DeleteDummyCommand
{
    public DeleteDummyCommand(int id)
    {
        Id = id;
    }

    [Required(ErrorMessage = "Id is required.")]
    public int Id { get; }
}