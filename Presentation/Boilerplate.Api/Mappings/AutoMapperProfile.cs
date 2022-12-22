using Boilerplate.Api.DTOs.Responses.Roles;
using Boilerplate.Api.DTOs.Responses.UserRoles;
using Boilerplate.Api.DTOs.Responses.Users;

namespace Boilerplate.Api.Mappings;

/// <summary>
///     AutoMapper profile for mapping between DTOs and entities
/// </summary>
public class AutoMapperProfile : Profile
{
    /// <summary>
    ///     Constructor for AutoMapperProfile
    /// </summary>
    public AutoMapperProfile()
    {
        CreateMap<UserDto, CreateUserResponse>();
        CreateMap<UserDto, UpdateUserResponse>();
        CreateMap<UserDto, GetUserResponse>();
        CreateMap<DummyDto, GetDummyResponse>().ReverseMap();
        CreateMap<DummyDto, CreateDummyResponse>().ReverseMap();
        CreateMap<DummyDto, UpdateDummyResponse>().ReverseMap();
        CreateMap<RoleDto, CreateRoleResponse>();
        CreateMap<RoleDto, UpdateRoleResponse>();
        CreateMap<RoleDto, GetRoleResponse>();
        CreateMap<UserRoleWithNamesDto, GetUserRoleResponse>();
        CreateMap<UserRoleDto, CreateUserRoleResponse>();
        CreateMap<UserRoleDto, UpdateUserRoleResponse>();
    }
}