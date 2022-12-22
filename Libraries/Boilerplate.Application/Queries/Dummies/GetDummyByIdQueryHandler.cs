using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Application.Queries.Dummies;

public class GetDummyByIdQueryHandler : IRequestHandler<GetDummyByIdQuery, DummyDto>
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public GetDummyByIdQueryHandler(DataContext db, IMapper mapper)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<DummyDto> Handle(GetDummyByIdQuery request, CancellationToken cancellationToken)
    {
        var dummy = await _db.Dummies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        return _mapper.Map<DummyDto>(dummy);
    }
}