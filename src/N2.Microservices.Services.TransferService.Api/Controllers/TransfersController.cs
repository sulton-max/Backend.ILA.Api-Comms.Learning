using MediatR;
using Microsoft.AspNetCore.Mvc;
using N2.Microservices.Common.Core.Models.Queries;
using N2.Microservices.Services.TransferService.Application.Transfers.Queries;
using N2.Microservices.Services.TransferService.Domain.Models.Entities;

namespace N2.Microservices.Services.TransferService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransfersController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransfersController(IMediator mediator, IServiceProvider serviceProvider)
    {
        var testA = serviceProvider.GetService<GetAllTransfersQuery>();
        var testB = serviceProvider.GetService<IQuery<IList<Transfer>>>();

        _mediator = mediator;
    }

    [HttpGet]
    public async ValueTask<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetAllTransfersQuery());
        return result.Any() ? Ok(result) : NotFound();
    }
}