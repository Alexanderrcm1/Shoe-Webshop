using System.ComponentModel.DataAnnotations;

namespace WebshopLibrary.DTOs;

public class UserDTO
{
	public string? Id { get; set; }
	[Required]
	[EmailAddress(ErrorMessage = "Not a valid email address")]
	public string? Email { get; set; }

	[Required, Compare(nameof(Email), ErrorMessage = "Email does not match")]
	public string? ConfirmEmail { get; set; }
	[Required]
	[RegularExpression("^(?=.*[A-Z])(?=.*\\d)(?=.*\\W).+$", ErrorMessage = "Password must contain 1 upper case letter, 1 number and 1 non alphanumeric character")]
	public string? Password { get; set; }
	[Required, Compare(nameof(Password), ErrorMessage = "Password does not match")]
	public string? ConfirmPassword { get; set; }
}