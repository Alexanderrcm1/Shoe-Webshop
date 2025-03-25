using System.Security.Claims;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebshopBackend.Endpoints;
using WebshopBackend.Exchange;
using WebshopBackend.Model;
using WebshopLibrary.DTOs;

namespace WebshopBackend
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddAuthorization();
			builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
				.AddCookie(IdentityConstants.ApplicationScheme);

			builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<WebshopContext>().AddApiEndpoints();


			builder.Services.AddDbContext<WebshopContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("WebshopDb")));

			builder.Services.AddHttpClient("ExchangeApi", client =>
			{
				client.BaseAddress = new Uri("https://v6.exchangerate-api.com/v6/");
			});

			builder.Services.AddScoped<IExchangeService, ExchangeService>();

			var app = builder.Build();


			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			using (var scope = app.Services.CreateScope())
			{
				var db = scope.ServiceProvider.GetRequiredService<WebshopContext>();
				db.Database.EnsureDeleted();
				db.Database.EnsureCreated();
				DbExtensions.PopulateDb(db);
			}

			app.WebshopEndpoints();

			app.MapGroup("/Account").MapIdentityApi<User>();
			app.MapGroup("/Account").MapGet("/AuthenticatedUser",
				async Task<IResult> (ClaimsPrincipal user, WebshopContext context) =>
				{
					string userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
					if (string.IsNullOrEmpty(userId))
					{
						return TypedResults.BadRequest("User ID claim is missing");
					}

					var webshopUser = await context.Users.FindAsync(userId);
					if (webshopUser == null)
					{
						return TypedResults.NotFound("User not found");
					}

					var userDTO = webshopUser.Adapt<UserDTO>();
					return TypedResults.Ok(userDTO);
				}).RequireAuthorization();
			app.Run();
		}
	}

	public static class DbExtensions
	{
		public static readonly List<Shoe> Shoes =
		[
			new Shoe()
				{ ModelId = 1, Brand = "Nike", Name = "Air Force", Color = "White", Size = "41", ImgUrl = "https://ballzy.eu/media/catalog/product/cache/1f82a4a518c32fa549b5e76a8b4522e3/n/i/nike-air-force-1-07-valge-cmp1.jpg", Price = 1499, Quantity = 3, OnSale = false},
			new Shoe()
				{ModelId = 2, Brand = "Nike", Name = "Air Jordan 1 Low", Color = "Black/White", Size = "43", ImgUrl = "https://ballzy.eu/media/catalog/product/cache/1f82a4a518c32fa549b5e76a8b4522e3/j/o/jordan-wmns-air-jordan-1-low-must-ry0j.jpg", Price = 1649, Quantity = 2, OnSale = false},
			new Shoe()
				{ ModelId = 3, Brand = "Nike", Name = "Dunk Low", Color = "White/Grey", Size = "39", ImgUrl = "https://ballzy.eu/media/catalog/product/cache/1f82a4a518c32fa549b5e76a8b4522e3/n/i/nike-nike-dunk-low-retro-hall-li1j.jpg", Price = 1649, Quantity = 3, OnSale = false},
			new Shoe()
				{ ModelId = 4, Brand = "New Balance", Name = "550", Color = "White/Beige", Size = "42", ImgUrl = "https://ballzy.eu/media/catalog/product/cache/1f82a4a518c32fa549b5e76a8b4522e3/n/e/new-balance-new-balance-550-valge-rczo.jpg", Price = 1399, Quantity = 1, OnSale = false},
			new Shoe()
				{ ModelId = 5, Brand = "Nike", Name = "Air Max 90", Color = "White", Size = "42", ImgUrl = "https://ballzy.eu/media/catalog/product/cache/1f82a4a518c32fa549b5e76a8b4522e3/n/i/nike-wmns-air-max-90-valge-tlqw.jpg", Price = 1295, Quantity = 1, OnSale = true},
			new Shoe()
				{ ModelId = 6, Brand = "Nike", Name = "Air Jordan 1 Mid", Color = "Blue/Yellow", Size = "39", ImgUrl = "https://ballzy.eu/media/catalog/product/cache/1f82a4a518c32fa549b5e76a8b4522e3/j/o/jordan-air-jordan-1-mid-gs-must-ujbn.jpg", Price = 1379, Quantity = 3, OnSale = false},
			new Shoe()
				{ ModelId = 7, Brand = "Nike", Name = "Air Jordan 1 Low", Color = "White", Size = "42", ImgUrl = "https://ballzy.eu/media/catalog/product/cache/1f82a4a518c32fa549b5e76a8b4522e3/j/o/jordan-air-jordan-1-low-gs-valge-7aws.jpg", Price = 1799, Quantity = 4, OnSale = false},
			new Shoe()
				{ ModelId = 8, Brand = "Nike", Name = "Air Jordan 1 Low", Color = "White/Pink", Size = "40", ImgUrl = "https://ballzy.eu/media/catalog/product/cache/1f82a4a518c32fa549b5e76a8b4522e3/j/o/jordan-air-jordan-1-low-gs-roosa-s1bo.jpg", Price = 1499, Quantity = 5, OnSale = false},
			new Shoe()
				{ ModelId = 9, Brand = "Nike", Name = "Dunk Low", Color = "White/Black", Size = "42", ImgUrl = "https://ballzy.eu/media/catalog/product/cache/1f82a4a518c32fa549b5e76a8b4522e3/n/i/nike-w-nike-dunk-low-must-bmkz.jpg", Price = 1899, Quantity = 6, OnSale = false},
			new Shoe()
				{ ModelId = 10, Brand = "Nike", Name = "Air Force", Color = "Black", Size = "39", ImgUrl = "https://ballzy.eu/media/catalog/product/cache/1f82a4a518c32fa549b5e76a8b4522e3/n/i/nike-wmns-air-force-1-07-must-ou3e.jpg", Price = 1345, Quantity = 1, OnSale = true},
			new Shoe() 
				{ModelId = 11, Brand = "Adidas", Name = "Campus 00s", Color = "Grey/White", Size = "37", ImgUrl = "https://ballzy.eu/media/catalog/product/cache/1149c092bad9f285b784d5aa1331d7cc/a/d/adidas-campus-00s-hall-mlu4.jpg", Price = 1199, Quantity = 2, OnSale = false},
			new Shoe() 
				{ModelId = 12, Brand = "New Balance", Name = "M1000", Color = "Black", Size = "44", ImgUrl = "https://ballzy.eu/media/catalog/product/cache/1149c092bad9f285b784d5aa1331d7cc/n/e/new-balance-new-balance-m1000-must-x0rd.jpg", Price = 2099, Quantity = 1, OnSale = false},



];
		public static void PopulateDb(WebshopContext db)
		{
			foreach (var shoe in Shoes)
			{
				db.Shoes.Add(shoe);
			}

			db.SaveChanges();
		}
	}
}
