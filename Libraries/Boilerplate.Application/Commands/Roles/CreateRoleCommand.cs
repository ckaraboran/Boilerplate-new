namespace Boilerplate.Application.Commands.Roles;

public class CreateRoleCommand : IRequest<RoleDto>
{
    public CreateRoleCommand(string name)
    {
        Name = name;
    }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; }
}