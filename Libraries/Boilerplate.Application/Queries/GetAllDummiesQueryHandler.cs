using MediatR;

namespace Boilerplate.Application.Queries;

public class GetAllDummiesQueryHandler : IRequestHandler<GetAllDummiesQuery, List<DummyDto>>
{
    private readonly IGenericRepository<Domain.Entities.Dummy> _dummyRepository;
    private readonly IMapper _mapper;

    public GetAllDummiesQueryHandler(IGenericRepository<Domain.Entities.Dummy> dummyRepository, IMapper mapper)
    {
        _dummyRepository = dummyRepository ?? throw new ArgumentNullException(nameof(dummyRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<DummyDto>> Handle(GetAllDummiesQuery request, CancellationToken cancellationToken)
    {
        var dummies = await _dummyRepository.GetAllAsync();
        return _mapper.Map<List<DummyDto>>(dummies);
    }
}