using System.ComponentModel.DataAnnotations;
using WebshopLibrary;

namespace WebshopBackend.Model;

public class Order
{
	public int Id { get; set; }
	[Required]
	public string? FirstName { get; set; }
	[Required]
	public string? LastName { get; set; }
	[EmailAddress, Required]
	public string? Email { get; set; }
	[Phone, Required]
	public string? Phone { get; set; }
	[Required]
	public string? Street { get; set; }
	[Required]
	public string? Postcode { get; set; }
	[Required]
	public string? Country { get; set; }
	[Required]
	public string? City { get; set; }

	public string? CartItems { get; set; }
}