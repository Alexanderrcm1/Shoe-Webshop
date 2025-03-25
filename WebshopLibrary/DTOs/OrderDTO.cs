using System.ComponentModel.DataAnnotations;

namespace WebshopLibrary.DTOs;

public class OrderDTO
{
	public int Id { get; set; }
	[Required]
	public string FirstName { get; set; } = string.Empty;
	[Required]
	public string LastName { get; set; } = string.Empty;
	[EmailAddress, Required]
	public string Email { get; set; } = string.Empty;
	[Phone, Required]
	public string Phone { get; set; } = string.Empty;
	[Required]
	public string Street { get; set; } = string.Empty;
	[Required]
	public string Postcode { get; set; } = string.Empty;
	[Required]
	public string Country { get; set; } = string.Empty;
	[Required]
	public string City { get; set; } = string.Empty;
	public string? CartItems { get; set; }

}