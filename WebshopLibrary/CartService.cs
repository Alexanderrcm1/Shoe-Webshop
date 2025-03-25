using System.Net.Http.Json;
using System.Text.Json;
using WebshopLibrary.DTOs;

namespace WebshopLibrary;

public class CartService
{
	public List<ShoeDTO> Cart { get; set; } = [];

	public async Task AddShoeToCart(string user, ShoeDTO shoe, HttpClient client)
	{
		Cart.Add(shoe);
		await MinusOneShoe(shoe, client);
		await UpdateCart(user, client);
	}

	public async Task RemoveItem(string user, ShoeDTO shoe, HttpClient client)
	{
		Cart.RemoveAt(Cart.FindIndex(m => m.Id == shoe.Id));
		await PlusOneShoe(shoe, client);
		await UpdateCart(user, client);
	}

	public async Task RemoveCart(string user, HttpClient client)
	{
		Cart = [];
		await client.DeleteAsync($"/cart/delete/{user}");
	}

	private static async Task MinusOneShoe(ShoeDTO shoe, HttpClient client)
	{
		await client.PutAsync($"/shoes/minus/{shoe.Id}", null);
	}

	private static async Task PlusOneShoe(ShoeDTO shoe, HttpClient client)
	{
		await client.PutAsync($"/shoes/plus/{shoe.Id}", null);
	}

	public async Task UpdateCart(string user, HttpClient client)
	{
		var userCart = new CartDTO
		{
			Name = user,
			CartItems = JsonSerializer.Serialize(Cart)
		};
		Console.WriteLine(userCart.CartItems);
		var response = await client.PostAsJsonAsync("/cart", userCart);
		Console.WriteLine(response);
	}
}
