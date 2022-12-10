using Kynetix.Common;

namespace Trade.Api;

public class Trade
{
    public Guid Id { get; set; }
    public TransactionType TransactionType { get; set; } = TransactionType.Future;
    public char? OptionTypeCode { get; set; }

    public bool BuySell { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTimeOffset TransactionDate { get; set; }

    public decimal? StrikePrice { get; set; }
    public DateTimeOffset? ExpiryDate { get; set; }
    public decimal? MarketValue { get; set; }
    public string? AccountCode { get; set; }
    public Guid AccountId { get; set; }
    public string? CurrencyCode { get; set; }
    public Guid CurrencyId { get; set; }
    public string? ExchangeCode { get; set; }
    public Guid ExchangeId { get; set; }
    public string? FirmCodeCode { get; set; }
    public Guid FirmId { get; set; }

    public string? InstrumentCode { get; set; }
    public Guid InstrumentId { get; set; }

}

public enum TransactionType
{
    Option = 0,
    Future = 1,
}
