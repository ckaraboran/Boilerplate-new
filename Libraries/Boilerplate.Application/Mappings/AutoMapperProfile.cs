using Boilerplate.Application.Commands;

namespace Boilerplate.Application.Mappings;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<DummyDto, CreateDummyCommand>().ReverseMap();
        CreateMap<CreateDummyCommand, Dummy>();
        CreateMap<DummyDto, UpdateDummyCommand>().ReverseMap();
    }
}