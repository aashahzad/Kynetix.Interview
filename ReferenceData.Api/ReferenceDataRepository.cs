using System.Collections.Generic;
using Kynetix.Common;

namespace ReferenceData.Api;

public interface IReferenceDataRepository
{
    Account GetAccountCode(string accountCode);
    IEnumerable<Account> GetAccounts();
    Currency GetCurrencyCode(string currencyCode);
    IEnumerable<Currency> GetCurrencies();
    Exchange GetExchangeCode(string exchangeCode);
    IEnumerable<Exchange> GetExchanges();
    Firm GetFirmCode(string firmCode);
    IEnumerable<Firm> GetFirms();
    Instrument GetInstrumentCode(string instrumentCode);
    IEnumerable<Instrument> GetInstruments();
}

public class ReferenceDataRepository : IReferenceDataRepository
{
    private readonly ReferenceDataDbContext _context;

    public ReferenceDataRepository(ReferenceDataDbContext context)
    {
        _context = context;
    }
    public Exchange GetExchangeCode(string exchangeCode)
    {
        var exchange = _context.Exchanges.Where(e => e.Code == exchangeCode).FirstOrDefault();
        //if the exchange code is not found in the dictionary, add a new exchange to the dictionary and return the exchange
        return exchange;
    }
    public IEnumerable<Exchange> GetExchanges()
    {
        var exchanges = _context.Exchanges.ToList();
        return exchanges;
    }
    public Firm GetFirmCode(string firmCode)
    {
        var firm = _context.Firms.Where(f => f.Code == firmCode).FirstOrDefault();
        return firm;
    }
    public IEnumerable<Firm> GetFirms()
    {
        var firms = _context.Firms.ToList();
        return firms;
    }
    public Account GetAccountCode(string accountCode)
    {
        var account = _context.Accounts.Where(a => a.Code == accountCode).FirstOrDefault();
        return account;
    }
    public IEnumerable<Account> GetAccounts()
    {
        var accounts = _context.Accounts.ToList();
        return accounts;
    }
    public Instrument GetInstrumentCode(string instrumentCode)
    {
        var instrument = _context.Instruments.Where(i => i.Code == instrumentCode).FirstOrDefault();
        return instrument;
    }
    public IEnumerable<Instrument> GetInstruments()
    {
        var instruments = _context.Instruments.ToList();
        return instruments;
    }
    public Currency GetCurrencyCode(string currencyCode)
    {
        var currency = _context.Currencies.Where(c => c.Code == currencyCode).FirstOrDefault();
        return currency;
    }
    public IEnumerable<Currency> GetCurrencies()
    {
        var currencies = _context.Currencies.ToList();
        return currencies;
    }
}