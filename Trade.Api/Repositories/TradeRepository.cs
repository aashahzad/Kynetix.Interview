using Microsoft.EntityFrameworkCore;

namespace Trade.Api;

public class TradeRepository : GenericRepository<Trade>, ITradeRepository
{
    public TradeRepository(TradeDbContext context) : base(context)
    {
    }

    public void AddTrade(Trade trade)
    {
        _context.Trades.Add(trade);
    }

    public Trade GetTrade(Guid tradeId)
    {
        return _context.Trades.FirstOrDefault(t => t.Id == tradeId);
    }

    public List<Trade> GetTrades()
    {
        return _context.Trades.ToList();
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }

}