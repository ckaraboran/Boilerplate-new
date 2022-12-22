using Boilerplate.Application.Commands.Dummies;
using Boilerplate.Application.Commands.Roles;
using Boilerplate.Application.Commands.UserRoles;
using Boilerplate.Application.Commands.Users;
using Boilerplate.Infrastructure.Security;

namespace Boilerplate.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Dummy, CreateDummyCommand>().ReverseMap();
        CreateMap<Dummy, DummyDto>().ReverseMap();
        CreateMap<Dummy, UpdateDummyCommand>().ReverseMap();
        CreateMap<Role, CreateRoleCommand>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<Role, UpdateRoleCommand>().ReverseMap();
        CreateMap<CreateUserCommand, User>()
            .ForMember(dest => dest.Password, opt
                => opt.MapFrom((src, dest)
                    => ClayPasswordHasher.HashPassword(dest, src.Password)))
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UpdateUserCommand, User>()
            .ForMember(dest => dest.Username, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
        CreateMap<UserRole, CreateUserRoleCommand>().ReverseMap();
        CreateMap<UserRole, UserRoleDto>().ReverseMap();
    }
}