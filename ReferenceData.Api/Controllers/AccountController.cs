using Microsoft.AspNetCore.Mvc;

namespace ReferenceData.Api;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IReferenceDataRepository _referenceDataRepository;

    public AccountController(IReferenceDataRepository referenceDataRepository)
    {
        _referenceDataRepository = referenceDataRepository;
    }
    [HttpGet("{accountCode}")]
    public IActionResult Get(string accountCode)
    {
        var account = _referenceDataRepository.GetAccountCode(accountCode);
        return Ok(account);
    }
    [HttpGet]
    public IActionResult Get()
    {
        var accounts = _referenceDataRepository.GetAccounts();
        return Ok(accounts);
    }
}