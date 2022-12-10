namespace Trade.Api;

public static class ConfigurationHelper
{
    public static IConfiguration Configuration { get; set; }

    public static void Initialiase(IConfiguration configuration)
    {
        Configuration = configuration;
    }
}