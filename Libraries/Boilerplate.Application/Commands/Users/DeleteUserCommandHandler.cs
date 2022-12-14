namespace Boilerplate.Application.Commands.Users;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IGenericRepository<User> _userRepository;

    public DeleteUserCommandHandler(IGenericRepository<User> userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user == null) throw new RecordNotFoundException($"User not found while deleting. UserId: '{request.Id}'");

        await _userRepository.DeleteAsync(user, cancellationToken);
        return Unit.Value;
    }
}