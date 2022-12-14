using Boilerplate.Api.DTOs.Responses.Roles;
using Boilerplate.Api.Security.Requirements;
using Boilerplate.Application.Commands.Roles;
using Boilerplate.Application.Queries.Roles;
using MediatR;

namespace Boilerplate.Api.Controllers;

/// <summary>
///     Endpoint for managing roles
/// </summary>
[Authorize(Policy = nameof(AuthorizationRequirement))]
[Route("api/roles")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    /// <summary>
    ///     Constructor for RoleController
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="mapper"></param>
    public RoleController(ISender mediator, IMapper mapper)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <summary>
    ///     Get all roles
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetRoleResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<GetRoleResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(void))]
    [Authorize(Policy = nameof(KnownRolesRequirement))]
    [HttpGet]
    public async Task<ActionResult<List<GetRoleResponse>>> GetAsync()
    {
        var result = await _mediator.Send(new GetRolesQuery());
        return Ok(_mapper.Map<List<GetRoleResponse>>(result));
    }

    /// <summary>
    ///     Get role by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Role with the specific id</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetRoleResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GetRoleResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(void))]
    [HttpGet("{id}")]
    [Authorize(Policy = nameof(KnownRolesRequirement))]
    public async Task<ActionResult<GetRoleResponse>> GetAsync(int id)
    {
        var result = await _mediator.Send(new GetRoleByIdQuery(id));
        return Ok(_mapper.Map<GetRoleResponse>(result));
    }

    /// <summary>
    ///     Create a new role
    /// </summary>
    /// <param name="createRoleCommand"></param>
    /// <returns>Created role</returns>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateRoleResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CreateRoleResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(void))]
    [HttpPost]
    [Authorize(Policy = nameof(SystemManagerRequirement))]
    public async Task<ActionResult<CreateRoleResponse>> PostAsync([FromBody] CreateRoleCommand createRoleCommand)
    {
        var result = await _mediator.Send(createRoleCommand);
        return Created(nameof(PostAsync), _mapper.Map<CreateRoleResponse>(result));
    }

    /// <summary>
    ///     Update a role
    /// </summary>
    /// <param name="updateRoleCommand"></param>
    /// <returns>Updated role</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateRoleResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(UpdateRoleResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
    [HttpPut]
    [Authorize(Policy = nameof(SystemManagerRequirement))]
    public async Task<ActionResult<UpdateRoleResponse>> PutAsync([FromBody] UpdateRoleCommand updateRoleCommand)
    {
        var result = await _mediator.Send(updateRoleCommand);
        return Ok(_mapper.Map<UpdateRoleResponse>(result));
    }

    /// <summary>
    ///     Delete a role
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Deletion result</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
    [HttpDelete("{id}")]
    [Authorize(Policy = nameof(SystemManagerRequirement))]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _mediator.Send(new DeleteRoleCommand(id));
        return Ok();
    }
}