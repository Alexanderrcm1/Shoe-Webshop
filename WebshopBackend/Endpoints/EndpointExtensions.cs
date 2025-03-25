using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebshopBackend.Exchange;
using WebshopBackend.Model;
using WebshopLibrary.DTOs;

namespace WebshopBackend.Endpoints;

public static class EndpointExtensions
{
	public static WebApplication WebshopEndpoints(this WebApplication app)
	{
		app.MapGet("/", () => "Hello World");

		app.MapGet("/shoes",
			async (WebshopContext context) => await context.Shoes.Select(shoe => shoe.Adapt<ShoeDTO>()).ToListAsync());
		app.MapGet("/shoes/{id}",
			async (WebshopContext context, int id) => await context.Shoes.Where(s => s.Id == id)
				.Select(s => s.Adapt<ShoeDTO>()).FirstOrDefaultAsync());
		app.MapPut("/shoes/minus/{id}", async (WebshopContext context, int id) =>
		{
			var shoeToEdit = await context.Shoes.FirstOrDefaultAsync(s => s.Id == id);
			if (shoeToEdit == null)
			{
				return Results.NotFound();
			}

			if (shoeToEdit.Quantity != 0)
			{
				shoeToEdit.Quantity -= 1;
			}
			await context.SaveChangesAsync();
			return Results.NoContent();
		});
		app.MapPut("/shoes/plus/{id}", async (WebshopContext context, int id) =>
		{
			var shoeToEdit = await context.Shoes.FirstOrDefaultAsync(s => s.Id == id);
			if (shoeToEdit == null)
			{
				return Results.NotFound();
			}

			shoeToEdit.Quantity += 1;

			await context.SaveChangesAsync();
			return Results.NoContent();
		});
		app.MapDelete("/shoes/delete/{shoe}", async (WebshopContext context, int shoe) =>
		{
			var shoeToDelete = await context.Shoes.FirstOrDefaultAsync(s => s.ModelId == shoe);

			if (shoeToDelete == null)
			{
				return Results.NotFound();
			}
			context.Shoes.Remove(shoeToDelete);
			await context.SaveChangesAsync();
			return Results.Ok();
		});

		app.MapGet("/orders", async (WebshopContext context) => await context.Orders.Select(order => order.Adapt<OrderDTO>()).ToListAsync());
		app.MapGet("/orders/{id}",
			async (WebshopContext context, int id) => await context.Orders.Where(o => o.Id == id)
				.Select(o => o.Adapt<OrderDTO>()).FirstOrDefaultAsync());
		app.MapGet("/orders/latest",
			async (WebshopContext context) => await context.Orders.OrderByDescending(order => order.Id).FirstOrDefaultAsync());
		app.MapPost("/orders", (WebshopContext context, Order order) =>
		{
			context.Orders.Add(order);
			context.SaveChanges();
		});

		app.MapGet("/cart",
			async (WebshopContext context) => await context.Carts.Select(c => c.Adapt<CartDTO>()).ToListAsync());
		app.MapGet("/cart/{name}",
			async (WebshopContext context, string name) => await context.Carts.Where(c => c.Name == name).Select(c => c.Adapt<CartDTO>()).FirstOrDefaultAsync());
		app.MapPost("/cart", async (WebshopContext context, Cart cart) =>
		{
			var existingCart = await context.Carts.FirstOrDefaultAsync(c => c.Name == cart.Name);

			if (existingCart != null)
			{
				existingCart.CartItems = cart.CartItems;
			}
			else
			{
				var newCart = new Cart
				{
					Name = cart.Name,
					CartItems = cart.CartItems
				};
				context.Carts.Add(newCart);
			}
			await context.SaveChangesAsync();
		});
		app.MapDelete("cart/delete/{name}", async (WebshopContext context, string name) =>
		{
			var cartToDelete = await context.Carts.FirstOrDefaultAsync(c => c.Name == name);

			if (cartToDelete == null)
			{
				return Results.NotFound();
			}
			context.Carts.Remove(cartToDelete);
			await context.SaveChangesAsync();
			return Results.Ok();
		});

		app.MapGet("/exchange", async (IExchangeService exchangeService) =>
		{
			var exchangeRate = await exchangeService.GetExchangeAsync();
			if (exchangeRate != null)
			{
				return Results.Ok(exchangeRate);
			}
			else
			{
				return Results.NotFound();
			}
			//return exchangeRate is not null ? Results.Ok(exchangeRate) : Results.NotFound();
		});

		return app;
	}


}