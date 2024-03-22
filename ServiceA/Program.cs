using ServiceA.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    var httpClient = new HttpClient();
    httpClient.BaseAddress = new Uri("https://min-api.cryptocompare.com");
    var serviceResponse = await httpClient.GetAsync("/data/price?fsym=BTC&tsyms=USD");

    var responseContent = await serviceResponse.Content.ReadAsStringAsync();
    if (!serviceResponse.IsSuccessStatusCode)
    {
        throw new ApplicationException($"{serviceResponse.StatusCode}: {responseContent}");
    }

    var bitcoin = System.Text.Json.JsonSerializer.Deserialize<Bitcoin>(responseContent);

    return $"Current value is {bitcoin?.USD} USD.";
})
.WithName("GetCurrentBitcoinValue")
.WithOpenApi();

app.Run();
