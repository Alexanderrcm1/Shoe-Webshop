using System.Net.Http.Json;
using WebshopLibrary.DTOs;

namespace WebshopLibrary;

public class ExchangeService
{
	public async Task<Dictionary<string, decimal>> GetExchangeRates(HttpClient client)
	{
		var response = await client.GetFromJsonAsync<ExchangeRateDTO>("/exchange");
		var rates = response.ConversionRates;
		Console.WriteLine(rates);
		return rates;
	}

	public async Task Exchange(string currency, decimal rate, string newCurr, HttpClient client)
	{
		var response = await client.GetFromJsonAsync<ExchangeRateDTO>("/exchange");
		var rates = response.ConversionRates;
		Console.WriteLine(rates);
		rate = rates[currency];
		newCurr = currency;
	}
}