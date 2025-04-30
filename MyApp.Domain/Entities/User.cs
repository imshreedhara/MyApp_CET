public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public string? PasswordHash { get; set; } // Store hashed password
    public required string Role { get; set; }
}
