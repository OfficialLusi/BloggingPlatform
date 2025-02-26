namespace BloggingPlatform_BE.Application.DTOs;

public class UserDto
{
    public int UserId { get; set; } // primary key
    public Guid UserGuid { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserSurname { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string? UserPassword { get; set; } = string.Empty;
    public DateTime UserCreatedOn { get; set; }
    public string Salt { get; set; } = string.Empty;
    public string HashCode { get; set; } = string.Empty;
}
