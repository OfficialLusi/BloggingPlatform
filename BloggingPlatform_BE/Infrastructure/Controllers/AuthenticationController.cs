using BloggingPlatform_BE.Application.DTOs;
using BloggingPlatform_BE.Domain.Interfaces;
using LusiUtilsLibrary.Backend.Initialization;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform_BE.Infrastructure.Controllers;

[ApiController]
[Route("api/Authentication")]
public class AuthenticationController : Controller
{
    #region private fields
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IAuthenticationService _authService;
    #endregion

    #region constructor
    public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authService)
    {
        #region initialize checks
        InitializeChecks.InitialCheck(logger, "Logger cannot be null");
        InitializeChecks.InitialCheck(authService, "Authentication service cannot be null");
        #endregion

        _logger = logger;
        _authService = authService;
    }
    #endregion

    #region public api methods
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AuthenticateUser(UserDto user)
    {
        {
            try
            {
                UserDto? repoUser = _authService.AuthenticateUser(user);

                if (repoUser != null)
                {
                    // the returned user must not show the salt and the hashcode for security reasons
                    repoUser.Salt = "";
                    repoUser.HashCode = "";
                    // -- 

                    _logger.LogInformation("Authentication Controller - Authenticate user call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
                    return Ok(repoUser);
                }
                _logger.LogInformation("Authentication Controller - Authenticate user call executed succesfully but user was not authenticated");
                return NoContent(); // even if the user is not auth, send a success status code with empty content to handle the issue on frontend
            }
            catch (Exception ex)
            {
                _logger.LogError("Blogging Platform Controller - Authenticate user call not executed. {exMessage}", ex.Message);
                return BadRequest();
            }
        }
    }
    #endregion
}
