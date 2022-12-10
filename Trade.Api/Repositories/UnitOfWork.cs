

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Api;
public class UnitOfWork: IUnitOfWork
{
    private readonly TradeDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    public ITradeRepository Trades { get; private set; }
    public UnitOfWork(TradeDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
        Trades = _serviceProvider.GetService<ITradeRepository>();
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

