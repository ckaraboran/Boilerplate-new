namespace Boilerplate.Application.Commands.Dummies;

public class CreateDummyCommand : IRequest<DummyDto>
{
    public CreateDummyCommand(string name)
    {
        Name = name;
    }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; }
}