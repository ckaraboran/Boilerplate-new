namespace Boilerplate.Application.Queries.Dummies;

public class GetDummyByIdQuery : IRequest<DummyDto>
{
    public GetDummyByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; }
}