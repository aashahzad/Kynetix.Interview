using Kynetix.Common;

namespace Trade.Api.Services;

public class TradeService : ITradeService
{
    protected HttpClient _referenceDataClient => UriHelper.Instance._referenceDataClient;
    protected HttpClient _marketDataClient => UriHelper.Instance._marketDataClient;


    private readonly IServiceProvider _serviceProvider;
    protected readonly IUnitOfWork _UnitOfWork;

    public TradeService()
    {

    }
    public TradeService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _UnitOfWork = _serviceProvider.GetService<IUnitOfWork>();
    }

    public virtual async Task<ApiResponse<Guid>> AddTrade(Trade trade)
    {
        var response = new ApiResponse<Guid>();
        //Return false if no transaction type is provided
        response = ValidateData(trade, response);
        if (!response.Success)
            return response;

        //Get Reference Data Values
        trade = await GetReferenceData(trade);

        //Call MarketData Service to get the Underlying Stock Price
        if (trade.TransactionType == TransactionType.Option)
        {
            var marketData = await _marketDataClient.GetFromJsonAsync<MarketDataResponse>($"api/market-data/{trade.InstrumentCode}");

            //Call Option: Option Price - (Underlying Stock Current Price - Call Strike Price) Intrinsic Value
            var instrumentPrice = marketData?.Price;
            if (trade.OptionTypeCode == 'C')
            {
                trade.MarketValue = trade.Price - (instrumentPrice - trade.StrikePrice);
            }
            //Put Option: Option Price - (Put Strike Price - Underlying Stock Current Price) Intrinic Value
            if (trade.OptionTypeCode == 'P')
            {
                trade.MarketValue = trade.Price - (trade.StrikePrice - instrumentPrice);
            }

        }
        if (trade.TransactionType == TransactionType.Future)
        {
            var marketData = await _marketDataClient.GetFromJsonAsync<MarketDataResponse>($"api/market-data/{trade.InstrumentCode}");
            Console.WriteLine(marketData);

            var instrumentPrice = marketData?.Price;
            trade.MarketValue = instrumentPrice;
        }
        else
        {
            trade.MarketValue = trade.Price;
        }

        //Add Issue to Database
        //var tradeRepository = new TradeRepository();
        //tradeRepository.AddTrade(trade);
        //tradeRepository.SaveChanges();
        _UnitOfWork.Trades.Add(trade);
        _UnitOfWork.SaveChanges();
        response.Data = trade.Id;
        return response;
    }

    public List<Trade> GetTrades()
    {
        //var tradeRepository = new TradeRepository();
        //var trades = tradeRepository.GetTrades();
        var trades = _UnitOfWork.Trades.GetAll().ToList();
        return trades;
    }

    public Trade GetTrade(Guid id)
    {
        //var tradeRepository = new TradeRepository();
        //var trade = tradeRepository.GetTrade(id);
        var trade = _UnitOfWork.Trades.GetById(id);
        return trade;
    }

    #region Private Methods

    protected ApiResponse<Guid> ValidateData(Trade trade, ApiResponse<Guid> response)
    {
        //if (string.IsNullOrEmpty(trade.TransactionType))
        //{
        //    throw new ArgumentException("Transaction type is required");
        //}
        if (trade.Quantity <= 0 || trade.Price <= 0)
        {
            response.ErrorMessage = "Quantity and Price must be greater than 0";
            return response;
        }
        var now = DateTimeOffset.Now;
        // Check Trade Date is not in the past
        if (trade.ExpiryDate is not null && trade.ExpiryDate < now)
        {
            response.ErrorMessage = "Expiry date cannot be in the past";
            return response;
        }
        // Check Trade Date is not in the future
        if (trade.TransactionDate > now)
        {
            response.ErrorMessage = "Transaction date cannot be in the future";
            return response;
        }
        response.Success = string.IsNullOrEmpty(response.ErrorMessage);
        return response;
    }
    protected async Task<Trade> GetReferenceData(Trade trade)
    {
        if (!string.IsNullOrEmpty(trade.ExchangeCode))
        {
            var exchange = await _referenceDataClient.GetFromJsonAsync<Exchange>($"api/exchange/{trade.ExchangeCode}");
            trade.ExchangeId = exchange?.Id ?? Guid.Empty;
        }
        if (!string.IsNullOrEmpty(trade.FirmCodeCode))
        {
            var firm = await _referenceDataClient.GetFromJsonAsync<Firm>($"api/firm/{trade.FirmCodeCode}");
            trade.FirmId = firm?.Id ?? Guid.Empty;

        }
        if (!string.IsNullOrEmpty(trade.AccountCode))
        {
            var account = await _referenceDataClient.GetFromJsonAsync<Account>($"api/account/{trade.AccountCode}");

            trade.AccountId = account?.Id ?? Guid.Empty;
        }
        if (!string.IsNullOrEmpty(trade.InstrumentCode))
        {
            var instrument = await _referenceDataClient.GetFromJsonAsync<Instrument>($"api/instrument/{trade.InstrumentCode}");

            trade.InstrumentId = instrument?.Id ?? Guid.Empty;

        }
        if (!string.IsNullOrEmpty(trade.CurrencyCode))
        {
            var currency = await _referenceDataClient.GetFromJsonAsync<Currency>($"api/currency/{trade.CurrencyCode}");

            trade.CurrencyId = currency?.Id ?? Guid.Empty;
        }

        return trade;
    }

    #endregion
}

public interface IOptionTradeService : ITradeService
{

}
public class OptionTradeService : TradeService, IOptionTradeService
{
    public override async Task<ApiResponse<Guid>> AddTrade(Trade trade)
    {
        var response = new ApiResponse<Guid>();
        //Return false if no transaction type is provided
        response = ValidateData(trade, response);
        if (!response.Success)
            return response;

        //Get Reference Data Values
        trade = await GetReferenceData(trade);

        var marketData = await _marketDataClient.GetFromJsonAsync<MarketDataResponse>($"api/market-data/{trade.InstrumentCode}");

        //Call Option: Option Price - (Underlying Stock Current Price - Call Strike Price) Intrinsic Value
        var instrumentPrice = marketData?.Price;
        if (trade.OptionTypeCode == 'C')
        {
            trade.MarketValue = trade.Price - (instrumentPrice - trade.StrikePrice);
        }
        //Put Option: Option Price - (Put Strike Price - Underlying Stock Current Price) Intrinic Value
        if (trade.OptionTypeCode == 'P')
        {
            trade.MarketValue = trade.Price - (trade.StrikePrice - instrumentPrice);
        }


        //Add Issue to Database
        //var tradeRepository = new TradeRepository();
        //tradeRepository.AddTrade(trade);
        //tradeRepository.SaveChanges();
        _UnitOfWork.Trades.Add(trade);
        _UnitOfWork.SaveChanges();
        response.Data = trade.Id;
        return response;
    }

}
public interface IFutureTradeService:ITradeService
{

}
public class FutureTradeService : TradeService, IFutureTradeService
{
    public override async Task<ApiResponse<Guid>> AddTrade(Trade trade)
    {
        var response = new ApiResponse<Guid>();
        //Return false if no transaction type is provided
        response = ValidateData(trade, response);
        if (!response.Success)
            return response;

        //Get Reference Data Values
        trade = await GetReferenceData(trade);

        var marketData = await _marketDataClient.GetFromJsonAsync<MarketDataResponse>($"api/market-data/{trade.InstrumentCode}");
        Console.WriteLine(marketData);

        var instrumentPrice = marketData?.Price;
        trade.MarketValue = instrumentPrice;

        //Add Issue to Database
        //var tradeRepository = new TradeRepository();
        //tradeRepository.AddTrade(trade);
        //tradeRepository.SaveChanges();
        _UnitOfWork.Trades.Add(trade);
        _UnitOfWork.SaveChanges();
        response.Data = trade.Id;
        return response;
    }

}