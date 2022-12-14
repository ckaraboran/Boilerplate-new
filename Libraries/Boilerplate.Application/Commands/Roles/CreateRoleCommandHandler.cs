namespace Boilerplate.Application.Commands.Roles;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Role> _roleRepository;

    public CreateRoleCommandHandler(IGenericRepository<Role> roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var existingRole = await _roleRepository.GetAsync(s => s.Name == request.Name, cancellationToken);

        if (existingRole != null)
            throw new RecordAlreadyExistsException($"There is a role with the same name: '{request.Name}'");

        var role = await _roleRepository.AddAsync(_mapper.Map<Role>(request), cancellationToken);

        return _mapper.Map<RoleDto>(role);
    }
}