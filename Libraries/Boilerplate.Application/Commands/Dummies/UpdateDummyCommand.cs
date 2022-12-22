namespace Boilerplate.Application.Commands.Dummies;

public class UpdateDummyCommand : IRequest<DummyDto>
{
    public UpdateDummyCommand(long id, string name)
    {
        Id = id;
        Name = name;
    }

    [Required(ErrorMessage = "Id is required.")]
    public long Id { get; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; }
}