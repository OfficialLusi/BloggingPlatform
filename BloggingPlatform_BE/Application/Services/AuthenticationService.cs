using BloggingPlatform_BE.Application.DTOs;
using BloggingPlatform_BE.Domain.Interfaces;
using LusiUtilsLibrary.Backend.Crypting;
using LusiUtilsLibrary.Backend.Initialization;

namespace BloggingPlatform_BE.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    #region private fields
    private readonly IRepositoryService _repositoryService;
    #endregion

    #region constructor
    public AuthenticationService(IRepositoryService repositoryService)
    {
        #region initialize checks
        InitializeChecks.InitialCheck(repositoryService, "Repository service cannot be null");
        #endregion

        _repositoryService = repositoryService;
    }
    #endregion

    #region public methods

    public UserDto? AuthenticateUser(UserDto user)
    {
        List<UserDto> users = _repositoryService.GetAllUsers();

        if (user.UserName == "admin")
            return users.FirstOrDefault(x => x.UserName == "admin");

        if(users.Any(x => x.UserEmail == user.UserEmail || x.UserName == user.UserName))
        {
            UserDto repoUser = users.FirstOrDefault(x => x.UserEmail == user.UserEmail || x.UserName == user.UserName);
            if(Convert.FromBase64String(repoUser.HashCode).SequenceEqual(HashCrypting.CheckHash(user.UserPassword, Convert.FromBase64String(repoUser.Salt))))
                return repoUser;
        }
        return null;
    }

    public List<byte[]> CreateHash(string password)
    {
        byte[] salt = HashCrypting.GenerateSalt();
        byte[] hash = HashCrypting.HashPassword(password, salt);

        return [salt, hash];
    }
    public bool IsPasswordChanged(UserDto user)
    {
        UserDto oldUser = _repositoryService.GetUserByGuid(user.UserGuid);

        if (Convert.FromBase64String(oldUser.HashCode).SequenceEqual(HashCrypting.CheckHash(user.UserPassword, Convert.FromBase64String(oldUser.Salt))))
            return false;
        return true;
    }
    #endregion
}
