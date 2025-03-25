using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using WebshopLibrary.DTOs;

namespace WebshopFrontend;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
	private readonly IHttpClientFactory _httpClientFactory;

	public CustomAuthStateProvider(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var client = _httpClientFactory.CreateClient("MyApi");

		try
		{
			var response = await client.GetAsync("/Account/AuthenticatedUser");

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var user = JsonSerializer.Deserialize<UserDTO>(json,
					new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
				if (user != null && !string.IsNullOrEmpty(user.Id))
				{
					var claims = new List<Claim>
					{
						new(ClaimTypes.NameIdentifier, user.Id),
						new(ClaimTypes.Email, user.Email ?? ""),
						new(ClaimTypes.Name, user.Email ?? "")
					};

					var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
					var userPrincipal = new ClaimsPrincipal(identity);
					return new AuthenticationState(userPrincipal);
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error fetching user: {ex.Message}");
		}

		return new AuthenticationState(new ClaimsPrincipal());
	}

	public async Task<bool> RegisterAsync(string email, string password)
	{
		var client = _httpClientFactory.CreateClient("MyApi");

		try
		{
			var json = JsonSerializer.Serialize(new { email, password });
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await client.PostAsync("/Account/register", content);

			if (response.IsSuccessStatusCode)
			{
				return true;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Register Error: {ex.Message}");
		}

		return false;
	}
	public async Task<bool> LogInAsync(string email, string password)
	{
		var client = _httpClientFactory.CreateClient("MyApi");

		try
		{
			var json = JsonSerializer.Serialize(new { email, password });
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await client.PostAsync("/Account/login?useCookies=true", content);

			if (response.IsSuccessStatusCode)
			{
				Task<AuthenticationState> authState = GetAuthenticationStateAsync();
				NotifyAuthenticationStateChanged(authState);
				return true;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Login error: {ex.Message}");
		}

		return false;
	}
}