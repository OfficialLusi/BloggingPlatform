namespace BloggingPlatform_FE.Models;

public class UserDto
{
    public int UserId { get; set; }
    public Guid UserGuid { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserSurname { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public String UserPassword { get; set; } = string.Empty;
    public DateTime UserCreatedOn { get; set; }
    public string Salt { get; set; } = string.Empty;
    public string HashCode { get; set; } = string.Empty;
}
