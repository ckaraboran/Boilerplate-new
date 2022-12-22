using Boilerplate.Api.Security.Requirements;
using Boilerplate.Application.Commands.Dummies;
using Boilerplate.Application.Queries.Dummies;
using MediatR;

namespace Boilerplate.Api.Controllers;

/// <summary>
///     Endpoints for managing dummies
/// </summary>
[Authorize(Policy = nameof(AuthorizationRequirement))]
[Route("api/dummies")]
[ApiController]
public class DummyController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    /// <summary>
    ///     Constructor for the DummyController
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="mapper"></param>
    public DummyController(ISender mediator, IMapper mapper)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <summary>
    ///     Get all dummies
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetDummyResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<GetDummyResponse>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(void))]
    [Authorize(Policy = nameof(KnownRolesRequirement))]
    [HttpGet]
    public async Task<ActionResult<List<GetDummyResponse>>> GetAsync()
    {
        var result = await _mediator.Send(new GetDummiesQuery());
        return Ok(_mapper.Map<List<GetDummyResponse>>(result));
    }

    /// <summary>
    ///     Get dummy by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Dummy with the specific id</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDummyResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GetDummyResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(void))]
    [HttpGet("{id}")]
    [Authorize(Policy = nameof(KnownRolesRequirement))]
    public async Task<ActionResult<GetDummyResponse>> GetAsync(long id)
    {
        var result = await _mediator.Send(new GetDummyByIdQuery(id));
        return Ok(_mapper.Map<GetDummyResponse>(result));
    }

    /// <summary>
    ///     Create a new dummy
    /// </summary>
    /// <param name="createDummyCommand"></param>
    /// <returns>Created dummy</returns>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateDummyResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CreateDummyResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(void))]
    [HttpPost]
    [Authorize(Policy = nameof(SystemManagerRequirement))]
    public async Task<ActionResult<CreateDummyResponse>> PostAsync([FromBody] CreateDummyCommand createDummyCommand)
    {
        var result = await _mediator.Send(createDummyCommand);
        return Created(nameof(PostAsync), _mapper.Map<CreateDummyResponse>(result));
    }

    /// <summary>
    ///     Update a dummy
    /// </summary>
    /// <param name="updateDummyCommand"></param>
    /// <returns>Updated dummy</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateDummyResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(UpdateDummyResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
    [HttpPut]
    [Authorize(Policy = nameof(SystemManagerRequirement))]
    public async Task<ActionResult<UpdateDummyResponse>> PutAsync([FromBody] UpdateDummyCommand updateDummyCommand)
    {
        var result = await _mediator.Send(updateDummyCommand);
        return Ok(_mapper.Map<UpdateDummyResponse>(result));
    }

    /// <summary>
    ///     Delete a dummy
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Deletion result</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
    [HttpDelete("{id}")]
    [Authorize(Policy = nameof(SystemManagerRequirement))]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        await _mediator.Send(new DeleteDummyCommand(id));
        return Ok();
    }
}