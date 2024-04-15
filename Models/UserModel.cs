namespace WebApi.Models;

public class UserModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
}