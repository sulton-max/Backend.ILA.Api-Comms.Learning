using MediatR;
using Microsoft.AspNetCore.Mvc;
using MilanJovanovich.RabbitMq.Products.Commands;
using MilanJovanovich.RabbitMq.Products.Queries;

namespace MilanJovanovich.RabbitMq.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllProductsQuery());
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("{productId:guid}")]
    public async ValueTask<IActionResult> GetById()
    {
        var result = await _mediator.Send(new GetProductByIdQuery());
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Get([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById),
            new
            {
                productId = result.Id
            },
            result);
    }
}