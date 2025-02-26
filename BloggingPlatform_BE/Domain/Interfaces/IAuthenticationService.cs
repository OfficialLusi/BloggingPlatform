using BloggingPlatform_BE.Application.DTOs;

namespace BloggingPlatform_BE.Domain.Interfaces;

public interface IAuthenticationService
{
    /// <summary>
    /// Create an hash code from the password
    /// </summary>
    /// <param name="password">the password of the user</param>
    /// <returns>a list with salt and hash code to save in db</returns>
    public List<byte[]> CreateHash(string password);

    /// <summary>
    /// If the user exists, it checks his hashcode 
    /// </summary>
    /// <param name="user">user object to authenticate</param>
    /// <returns>the user object authenticated</returns>
    public UserDto? AuthenticateUser(UserDto user);

    /// <summary>
    /// Checks if the password has been changed
    /// </summary>
    /// <param name="user">the user object to update</param>
    /// <returns>true if changed, false if not</returns>
    public bool IsPasswordChanged(UserDto user);
}
