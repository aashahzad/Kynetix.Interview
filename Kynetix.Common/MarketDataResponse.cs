namespace Kynetix.Common;

public class MarketDataResponse
{
    public MarketDataResponse(string instrumentCode, decimal price)
    {
        InstrumentCode = instrumentCode;
        Price = price;
    }

    public string InstrumentCode { get; init; }
    public decimal Price { get; init; }
}