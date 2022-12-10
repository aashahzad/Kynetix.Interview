using Microsoft.AspNetCore.Mvc;

namespace ReferenceData.Api;

[Route("api/[controller]")]
[ApiController]
public class CurrencyController : ControllerBase
{
    private readonly IReferenceDataRepository _referenceDataRepository;

    public CurrencyController(IReferenceDataRepository referenceDataRepository)
    {
        _referenceDataRepository = referenceDataRepository;
    }
    [HttpGet("{currencyCode}")]
    public IActionResult Get(string currencyCode)
    {
        var currency = _referenceDataRepository.GetCurrencyCode(currencyCode);
        return Ok(currency);
    }
    [HttpGet]
    public IActionResult Get()
    {
        var currencies = _referenceDataRepository.GetCurrencies();
        return Ok(currencies);
    }
}