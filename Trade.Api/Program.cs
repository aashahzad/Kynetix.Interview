using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Trade.Api;
using Trade.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationHelper.Initialiase(builder.Configuration);

builder.Services.AddDbContext<TradeDbContext>(options =>
options.UseInMemoryDatabase("TradeDb"),ServiceLifetime.Scoped);
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITradeRepository, TradeRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITradeService, TradeService>();
builder.Services.AddScoped<IFutureTradeService, FutureTradeService>();
builder.Services.AddScoped<IOptionTradeService, OptionTradeService>();

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/myApi/swagger/v1/swagger.json", "V1 Docs");

    //});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
