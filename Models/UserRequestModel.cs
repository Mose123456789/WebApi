using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class UserRequestModel
{
    [Required(ErrorMessage = "You need to input a valid first name")]
    [MinLength(2)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MinLength(2)]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}
