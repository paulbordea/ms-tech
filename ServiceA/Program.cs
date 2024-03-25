using ServiceA.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();
builder.Services.AddHostedService<TimedHostedService>();
builder.Services.AddScoped<IBitcoinService, BitcoinService>();
builder.Services.AddSingleton<IMemoryCacheService<double>, MemoryCacheService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/current", async () =>
    {
        using var serviceScope = app.Services.CreateScope();
        var services = serviceScope.ServiceProvider;
        var bitcoinService = services.GetRequiredService<IBitcoinService>();
        return await bitcoinService.GetBitcoinValue().ConfigureAwait(false);
    })
.WithName("GetCurrentBitcoinValue")
.WithOpenApi();

app.MapGet("/average", () =>
    {
        using var serviceScope = app.Services.CreateScope();
        var services = serviceScope.ServiceProvider;
        var memoryCacheService = services.GetRequiredService<IMemoryCacheService<double>>();
        
        return memoryCacheService.Get("average").ToString("##.##");
    })
    .WithName("GetCurrentAverageValue")
    .WithOpenApi();

app.Run();
