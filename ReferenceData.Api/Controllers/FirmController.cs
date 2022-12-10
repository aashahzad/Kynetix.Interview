using Microsoft.AspNetCore.Mvc;

namespace ReferenceData.Api;

[Route("api/[controller]")]
[ApiController]
public class FirmController : ControllerBase
{
    private readonly IReferenceDataRepository _referenceDataRepository;

    public FirmController(IReferenceDataRepository referenceDataRepository)
    {
        _referenceDataRepository = referenceDataRepository;
    }
    [HttpGet("{firmCode}")]
    public IActionResult Get(string firmCode)
    {
        var firm = _referenceDataRepository.GetFirmCode(firmCode);
        return Ok(firm);
    }
    [HttpGet]
    public IActionResult Get()
    {
        var firms = _referenceDataRepository.GetFirms();
        return Ok(firms);
    }
}
