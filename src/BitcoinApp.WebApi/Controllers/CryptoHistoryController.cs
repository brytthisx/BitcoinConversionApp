using BitcoinApp.Application.CryptoHistory;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BitcoinApp.Application.CryptoHistory.GetCryptoHistory;
using BitcoinApp.Infrastructure.Persistence.Database.MsSql;
using MassTransit;
using BitcoinApp.Application.CryptoHistory.UpdateCryptoHistory;
using BitcoinApp.Application.CryptoHistory.DeleteCryptoHistory;


namespace BitcoinApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[SwaggerTag(
    "Handles all operations related to bitcoin price history records, including creation, retrieval, updating, and deletion.")]
public class CryptoHistoryController(IMediator mediator, AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet("{recordId}")]
    [SwaggerOperation("Get a single record")]
    [ProducesResponseType(typeof(GetCryptoHistoryRecordDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRecord([FromRoute] GetCryptoHistoryRecordQuery query)
    {
        IRequestClient<GetCryptoHistoryRecordQuery>? client =
            mediator.CreateRequestClient<GetCryptoHistoryRecordQuery>();
        Response<GetCryptoHistoryRecordDto>? response = await client.GetResponse<GetCryptoHistoryRecordDto>(query);

        return Ok(response.Message);
    }

    [HttpGet]
    [SwaggerOperation("Get all records")]
    [ProducesResponseType(typeof(GetCryptoHistoryRecordsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRecords()
    {
        IRequestClient<GetCryptoHistoryRecordsQuery> client =
            mediator.CreateRequestClient<GetCryptoHistoryRecordsQuery>();
        Response<GetCryptoHistoryRecordsDto> response =
            await client.GetResponse<GetCryptoHistoryRecordsDto>(new GetCryptoHistoryRecordsQuery());

        return Ok(response.Message);
    }

    [HttpPost]
    [SwaggerOperation("Create a new record")]
    [ProducesResponseType(typeof(CreateCryptoHistoryCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateRecord([FromBody] CreateCryptoHistoryCommand request)
    {
        IRequestClient<CreateCryptoHistoryCommand>?
            client = mediator.CreateRequestClient<CreateCryptoHistoryCommand>();
        Response<CreateCryptoHistoryCommandResponse>? response =
            await client.GetResponse<CreateCryptoHistoryCommandResponse>(request);

        return CreatedAtAction(nameof(GetRecord), new { recordId = response.Message.HistoryId }, response.Message);
    }

    [HttpPatch("{historyId}")]
    [SwaggerOperation("Update a record's comment")]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRecordComment([FromRoute] UpdateCryptoHistoryCommand request)
    {
        IRequestClient<UpdateCryptoHistoryCommand>? client =
            mediator.CreateRequestClient<UpdateCryptoHistoryCommand>();
        Response<UpdateCryptoHistoryCommandResponse>? response =
            await client.GetResponse<UpdateCryptoHistoryCommandResponse>(request);

        return Ok(response.Message);
    }

    [HttpDelete("{historyId}")]
    [SwaggerOperation("Delete a record")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecord([FromRoute] DeleteCryptoHistoryCommand request)
    {
        IRequestClient<DeleteCryptoHistoryCommand>? client =
            mediator.CreateRequestClient<DeleteCryptoHistoryCommand>();
        Response<DeleteCryptoHistoryCommandResponse>? response =
            await client.GetResponse<DeleteCryptoHistoryCommandResponse>(request);

        return Ok(response.Message);
    }
}
