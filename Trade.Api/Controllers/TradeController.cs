using Microsoft.AspNetCore.Mvc;
using Trade.Api.Services;

namespace Trade.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TradeController : ControllerBase
{
    private readonly ITradeService _tradeService;
    private readonly IFutureTradeService _futureTradeServicce;
    public TradeController(ITradeService tradeService, IFutureTradeService futureTradeServicce)
    {
        _tradeService = tradeService;
        _futureTradeServicce = futureTradeServicce;
    }

    [HttpGet]
    [Route("GetTrades")]
    public IActionResult GetTrades()
    {
        //var tradeService = new TradeService();
        var trades = _tradeService.GetTrades();
        return Ok(new ApiResponse<List<Trade>>
        {
            Success = true,
            Data = trades
        });
    }

    //Get trades by trade id
    [HttpGet]
    [Route("GetById/{id}")]
    public IActionResult GetById(Guid id)
    {
        //var tradeService = new TradeService();
        var trade = _tradeService.GetTrade(id);
        if (trade == null)
        {
            return NotFound(new ApiResponse<Trade>
            {
                Success = false,
                ErrorMessage = "Trade not found"
            });
        }
        return Ok(new ApiResponse<Trade>
        {
            Success = true,
            Data = trade
        });
    }

    [HttpPost]
    [Route("SaveTrade")]
    public async Task<IActionResult> SaveTrade([FromBody] Trade trade)
    {
        try
        {
            //var tradeService = new TradeService();
            var response = await _tradeService.AddTrade(trade);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<Guid>
            {
                Success = false,
                ErrorMessage = $"Trade could not be added: {ex.Message}"
            });
        }
    }

    [HttpPost]
    [Route("SaveTradeByOption")]
    public async Task<IActionResult> SaveTradeByOption([FromBody] Trade trade)
    {
        try
        {
            //var tradeService = new TradeService();
            var response = await _futureTradeServicce.AddTrade(trade);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<Guid>
            {
                Success = false,
                ErrorMessage = $"Trade could not be added: {ex.Message}"
            });
        }
    }

}
