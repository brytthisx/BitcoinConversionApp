using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BitcoinApp.Application.Crypto.GetLatestConversion;

namespace BitcoinApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[SwaggerTag(
    "Handles all operations related to bitcoin price history records, including creation, retrieval, updating, and deletion.")]
public class CryptoController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get latest data from external API")]
    [ProducesResponseType(typeof(GetLatestConversionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLatestConversion(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetLatestConversionQuery(), cancellationToken);

        if (response is null)
            return NotFound();

        return Ok(response);
    }
}
