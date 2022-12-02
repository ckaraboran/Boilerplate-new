using MediatR;

namespace Boilerplate.Application.Queries;

public class GetDummyQueryHandler : IRequestHandler<GetDummyQuery, DummyDto>
{
    private readonly IGenericRepository<Domain.Entities.Dummy> _dummyRepository;
    private readonly IMapper _mapper;

    public GetDummyQueryHandler(IGenericRepository<Domain.Entities.Dummy> dummyRepository, IMapper mapper)
    {
        _dummyRepository = dummyRepository ?? throw new ArgumentNullException(nameof(dummyRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<DummyDto> Handle(GetDummyQuery request, CancellationToken cancellationToken)
    {
        var dummy = await _dummyRepository.GetAsync(request.Id);
        return _mapper.Map<DummyDto>(dummy);
    }
}