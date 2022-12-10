using System;

namespace MarketData.Api
{
    public interface IMarketDataService
    {
        void Dispose();
        decimal GetInstrumentPrice(string instrumentCode);
    }

    public class MarketDataService : IMarketDataService
    {
        public decimal GetInstrumentPrice(string instrumentCode)
        {
            //Simulate a call to a Market Data Service
            var random = new Random();
            return Math.Round((decimal)random.NextDouble() * 100, 2);
        }

        public void Dispose()
        {
            System.Console.WriteLine("MarketDataService Disposed");
        }
    }
}
