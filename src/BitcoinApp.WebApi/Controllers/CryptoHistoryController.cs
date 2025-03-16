using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BitcoinApp.Application.CryptoHistory.GetCryptoHistory;
using BitcoinApp.Application.CryptoHistory.CreateCryptoHistory;
using BitcoinApp.Application.CryptoHistory.UpdateCryptoHistory;
using BitcoinApp.Application.CryptoHistory.DeleteCryptoHistory;
using BitcoinApp.Domain.CryptoHistory;

namespace BitcoinApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[SwaggerTag(
    "Handles all operations related to bitcoin price history records, including creation, retrieval, updating, and deletion.")]
public class CryptoHistoryController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{historyId:guid}")]
    [SwaggerOperation("Get a single record")]
    [ProducesResponseType(typeof(GetCryptoHistoryRecordDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRecord(Guid historyId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCryptoHistoryRecordQuery(historyId), cancellationToken);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation("Get all records")]
    [ProducesResponseType(typeof(GetCryptoHistoryRecordsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRecords(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCryptoHistoryRecordsQuery(), cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation("Create a new record")]
    [ProducesResponseType(typeof(CreateCryptoHistoryCommandResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateRecord([FromBody] CreateCryptoHistoryCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(GetRecord), new { historyId = response.HistoryId.Value }, response);
    }

    [HttpPatch("{historyId:guid}")]
    [SwaggerOperation("Update a record's comment")]
    [ProducesResponseType(typeof(UpdateCryptoHistoryCommandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRecord(Guid historyId, [FromBody] UpdateCryptoHistoryDto data, CancellationToken cancellationToken)
    {
        var command = new UpdateCryptoHistoryCommand(new HistoryId(historyId), data);
        var response = await _mediator.Send(command, cancellationToken);

        return response != null ? Ok(response) : NotFound();
    }

    [HttpDelete("{historyId:guid}")]
    [SwaggerOperation("Delete a record")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecord(Guid historyId, CancellationToken cancellationToken)
    {
        var command = new DeleteCryptoHistoryCommand(new HistoryId(historyId));
        var response = await _mediator.Send(command, cancellationToken);

        return response != null ? Ok(response) : NotFound();
    }
}
