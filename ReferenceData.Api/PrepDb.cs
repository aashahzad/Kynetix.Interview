using System.Text.Json;
using Kynetix.Common;

namespace ReferenceData.Api;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<ReferenceDataDbContext>());
        }
    }

    private static void SeedData(ReferenceDataDbContext context)
    {
        if (!context.Accounts.Any())
        {
            Console.WriteLine("--> Seeding data...");

            var accountsFile = File.ReadAllText("../SampleData/Accounts.json");
            // Console.WriteLine(accountsFile);

            var accounts = JsonSerializer.Deserialize<List<Account>>(accountsFile);

            context.Accounts.AddRange(accounts);
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }
        if (!context.Currencies.Any())
        {
            var currenciesFile = File.ReadAllText("../SampleData/Currencies.json");
            // Console.WriteLine(currenciesFile);
            var currencies = JsonSerializer.Deserialize<List<Currency>>(currenciesFile);

            context.Currencies.AddRange(currencies);
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }
        if (!context.Exchanges.Any())
        {
            var exchangesFile = File.ReadAllText("../SampleData/Exchanges.json");
            // Console.WriteLine(exchangesFile);
            var exchanges = JsonSerializer.Deserialize<List<Exchange>>(exchangesFile);

            context.Exchanges.AddRange(exchanges);

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }
        if (!context.Firms.Any())
        {
            var firmsFile = File.ReadAllText("../SampleData/Firms.json");
            // Console.WriteLine(firmsFile);
            var firms = JsonSerializer.Deserialize<List<Firm>>(firmsFile);

            context.Firms.AddRange(firms);

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }
        if (!context.Instruments.Any())
        {
            var instrumentsFile = File.ReadAllText("../SampleData/Instruments.json");
            // Console.WriteLine(instrumentsFile);
            var instruments = JsonSerializer.Deserialize<List<Instrument>>(instrumentsFile);
            context.Instruments.AddRange(instruments);

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }

    }
}