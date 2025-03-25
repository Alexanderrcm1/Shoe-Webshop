using Azure;
using WebshopLibrary.DTOs;

namespace WebshopBackend.Exchange;

public interface IExchangeService
{
	Task<ExchangeRateDTO> GetExchangeAsync();
}

public class ExchangeService : IExchangeService
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly string _apiKey;

	public ExchangeService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
	{
		_httpClientFactory = httpClientFactory;
		_apiKey = configuration["ApiKeys:Exchange"] ?? throw new InvalidOperationException("ApiKey is not set");
	}

	public async Task<ExchangeRateDTO> GetExchangeAsync()
	{
		using var client = _httpClientFactory.CreateClient("ExchangeApi");
		string url = $"{_apiKey}/latest/SEK/";
		Console.WriteLine(url);

		return await client.GetFromJsonAsync<ExchangeRateDTO>(url);
	}
}