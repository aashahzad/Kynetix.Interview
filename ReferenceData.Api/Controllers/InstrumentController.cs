using Microsoft.AspNetCore.Mvc;

namespace ReferenceData.Api;

[Route("api/[controller]")]
[ApiController]
public class InstrumentController : ControllerBase
{
    private readonly IReferenceDataRepository _referenceDataRepository;

    public InstrumentController(IReferenceDataRepository referenceDataRepository)
    {
        _referenceDataRepository = referenceDataRepository;
    }
    [HttpGet("{instrumentCode}")]
    public IActionResult Get(string instrumentCode)
    {
        var instrument = _referenceDataRepository.GetInstrumentCode(instrumentCode);
        return Ok(instrument);
    }
    [HttpGet]
    public IActionResult Get()
    {
        var instruments = _referenceDataRepository.GetInstruments();
        return Ok(instruments);
    }
}