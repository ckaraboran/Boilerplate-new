namespace Boilerplate.Application.Commands.Dummies;

public class UpdateDummyCommandHandler : IRequestHandler<UpdateDummyCommand, DummyDto>
{
    private readonly IGenericRepository<Dummy> _dummyRepository;
    private readonly IMapper _mapper;

    public UpdateDummyCommandHandler(IGenericRepository<Dummy> dummyRepository, IMapper mapper)
    {
        _dummyRepository = dummyRepository ?? throw new ArgumentNullException(nameof(dummyRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<DummyDto> Handle(UpdateDummyCommand request, CancellationToken cancellationToken)
    {
        var existingDummy = await _dummyRepository.GetByIdAsync(request.Id);

        if (existingDummy == null)
            throw new RecordNotFoundException($"Dummy not found. DummyId: '{request.Id}'");

        var existingAnotherDummy = await _dummyRepository.GetAsync(s => s.Name == request.Name && s.Id != request.Id);

        if (existingAnotherDummy != null)
            throw new RecordAlreadyExistsException($"There is a dummy with the same name: '{request.Name}'");

        existingDummy = _mapper.Map<Dummy>(request);
        var updatedDummy = await _dummyRepository.UpdateAsync(existingDummy);

        return _mapper.Map<DummyDto>(updatedDummy);
    }
}