using Kynetix.Common;
using Microsoft.AspNetCore.Mvc;

namespace MarketData.Api;

[Route("api/market-data")]
[ApiController]
public class MarketDataController : ControllerBase
{
    private readonly IMarketDataService _marketDataService;
    public MarketDataController(IMarketDataService marketDataService)
    {
        _marketDataService = marketDataService;
    }

    //Get method for getting the market data for a given instrument
    [HttpGet("{instrumentCode}")]
    public IActionResult Get(string instrumentCode)
    {
        var price = _marketDataService.GetInstrumentPrice(instrumentCode);
        return Ok(new MarketDataResponse(instrumentCode, price));
    }
}