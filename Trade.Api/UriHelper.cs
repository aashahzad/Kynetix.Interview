using System.ComponentModel;

namespace Trade.Api;
public class UriHelper
{
    private static readonly UriHelper instance = new UriHelper();

    static UriHelper()
    {

    }

    public static UriHelper Instance => instance;

    public HttpClient _referenceDataClient => new()
    {
        BaseAddress = ConfigurationHelper.Configuration.GetServiceUri("referencedata-api") ?? new Uri("http://localhost:5210")
    };

    public HttpClient _marketDataClient => new()
    {
        BaseAddress = ConfigurationHelper.Configuration.GetServiceUri("marketdata-api") ?? new Uri("http://localhost:5197")
    };

}

