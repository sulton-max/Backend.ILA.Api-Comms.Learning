using MediatR;
using Microsoft.AspNetCore.Mvc;
using N2.Microservices.Services.BankingService.Application.Accounts.Queries;
using N2.Microservices.Services.BankingService.Application.Transfers.Commands;

namespace N2.Microservices.Services.BankingService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async ValueTask<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetAccountsQuery());
        return Ok(result);
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> Post([FromBody] TransferCommand transfer)
    {
        var result = await _mediator.Send(transfer);
        return Accepted(result);
    }
}