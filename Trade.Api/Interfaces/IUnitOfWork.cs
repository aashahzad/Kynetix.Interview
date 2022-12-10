using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Api;
    public interface IUnitOfWork : IDisposable
    {
        ITradeRepository Trades { get; }
        bool SaveChanges();
    }

