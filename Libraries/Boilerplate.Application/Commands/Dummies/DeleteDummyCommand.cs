namespace Boilerplate.Application.Commands.Dummies;

public class DeleteDummyCommand : IRequest
{
    public DeleteDummyCommand(long id)
    {
        Id = id;
    }

    [Required(ErrorMessage = "Id is required.")]
    public long Id { get; }
}