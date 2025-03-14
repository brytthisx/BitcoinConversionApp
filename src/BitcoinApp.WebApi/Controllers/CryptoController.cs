using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BitcoinApp.Application.Crypto.GetLatestConversion;
using MassTransit;

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
    public async Task<IActionResult> GetLatestConversion()
    {
        IRequestClient<GetLatestConversionQuery> client = mediator.CreateRequestClient<GetLatestConversionQuery>();
        Response<GetLatestConversionDto> response =
            await client.GetResponse<GetLatestConversionDto>(new GetLatestConversionQuery());

        return Ok(response.Message);
    }
}
