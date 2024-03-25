using ServiceA.Models;

namespace ServiceA.Services
{
    public class BitcoinService: IBitcoinService
    {
        public async Task<double> GetBitcoinValue()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://min-api.cryptocompare.com")
            };

            var serviceResponse = await httpClient.GetAsync("/data/price?fsym=BTC&tsyms=USD");

            var responseContent = await serviceResponse.Content.ReadAsStringAsync();
            if (!serviceResponse.IsSuccessStatusCode)
            {
                throw new ApplicationException($"{serviceResponse.StatusCode}: {responseContent}");
            }

            var bitcoin = System.Text.Json.JsonSerializer.Deserialize<Bitcoin>(responseContent);

            return bitcoin!.USD;
        }
    }
}
