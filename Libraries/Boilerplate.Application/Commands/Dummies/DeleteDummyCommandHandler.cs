namespace Boilerplate.Application.Commands.Dummies;

public class DeleteDummyCommandHandler : IRequestHandler<DeleteDummyCommand>
{
    private readonly IGenericRepository<Dummy> _dummyRepository;

    public DeleteDummyCommandHandler(IGenericRepository<Dummy> dummyRepository)
    {
        _dummyRepository = dummyRepository ?? throw new ArgumentNullException(nameof(dummyRepository));
    }

    public async Task<Unit> Handle(DeleteDummyCommand request, CancellationToken cancellationToken)
    {
        var dummy = await _dummyRepository.GetByIdAsync(request.Id, cancellationToken);

        if (dummy == null)
            throw new RecordNotFoundException($"Dummy not found while deleting. DummyId: '{request.Id}'");

        await _dummyRepository.DeleteAsync(dummy, cancellationToken);
        return Unit.Value;
    }
}