using MediatR;
using Microsoft.AspNetCore.Mvc;
using ThreePillers.AddressBook.Application.Bases.Responses;
using ThreePillers.AddressBook.Application.UseCases.Departments.Commands.Create;
using ThreePillers.AddressBook.Application.UseCases.Departments.Commands.Delete;
using ThreePillers.AddressBook.Application.UseCases.Departments.Commands.Update;
using ThreePillers.AddressBook.Application.UseCases.Departments.Dtos;
using ThreePillers.AddressBook.Application.UseCases.Departments.Queries.GetById;

namespace ThreePillers.AddressBook.API.Controllers;

[Route("api/v1/department")]
[ApiController]
public class DepartmentController(IMediator mediator) : ControllerBase
{
    [HttpPost()]
    public async Task<ActionResult<ResponseOf<DepartmentDto>>> AddAsync(
        [FromBody] CreateDepartmentCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));

    [HttpPut()]
    public async Task<ActionResult<ResponseOf<DepartmentDto>>> UpdateAsync(
        [FromBody] UpdateDepartmentCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));

    [HttpDelete("{id:long}")]
    public async Task<ActionResult<Response>> DeleteByIdAsync(
        [FromRoute] long id,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new DeleteDepartmentCommand(id), cancellationToken));

    [HttpGet("{id:long}")]
    public async Task<ActionResult<Response>> GetByIdAsync(
        [FromRoute] long id,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(new GetDepartmentByIdQuery(id), cancellationToken));
}
