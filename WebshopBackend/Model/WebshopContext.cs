using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebshopLibrary;

namespace WebshopBackend.Model;

public class WebshopContext : IdentityDbContext<User>
{
	public WebshopContext(DbContextOptions<WebshopContext> options) : base(options) { }
		public DbSet<Shoe> Shoes { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Cart> Carts { get; set; }
		
}