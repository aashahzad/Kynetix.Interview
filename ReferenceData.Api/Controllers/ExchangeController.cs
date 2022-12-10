using Microsoft.AspNetCore.Mvc;

namespace ReferenceData.Api;


[Route("api/[controller]")]
[ApiController]
public class ExchangeController : ControllerBase
{
    private readonly IReferenceDataRepository _referenceDataRepository;

    public ExchangeController(IReferenceDataRepository referenceDataRepository)
    {
        _referenceDataRepository = referenceDataRepository;
    }
    [HttpGet("{exchangeCode}")]
    public IActionResult Get(string exchangeCode)
    {
        var exchange = _referenceDataRepository.GetExchangeCode(exchangeCode);
        return Ok(exchange);
    }
    [HttpGet]
    public IActionResult Get()
    {
        var exchanges = _referenceDataRepository.GetExchanges();
        return Ok(exchanges);
    }
}
