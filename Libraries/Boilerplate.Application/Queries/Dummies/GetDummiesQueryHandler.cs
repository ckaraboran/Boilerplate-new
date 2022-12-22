using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Application.Queries.Dummies;

public class GetDummiesQueryHandler : IRequestHandler<GetDummiesQuery, List<DummyDto>>
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public GetDummiesQueryHandler(DataContext db, IMapper mapper)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<DummyDto>> Handle(GetDummiesQuery request, CancellationToken cancellationToken)
    {
        var dummies = await _db.Dummies.AsNoTracking().ToListAsync(cancellationToken);
        return _mapper.Map<List<DummyDto>>(dummies);
    }
}