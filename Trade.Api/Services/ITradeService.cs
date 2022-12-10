namespace Trade.Api;
public interface ITradeService
{
    Task<ApiResponse<Guid>> AddTrade(Trade trade);
    List<Trade> GetTrades();
    Trade GetTrade(Guid id);
}

