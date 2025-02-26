using BloggingPlatform_BE.Application.DTOs;
using BloggingPlatform_BE.Domain.Interfaces;
using LusiUtilsLibrary.Backend.Initialization;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform_BE.Infrastructure.Controllers;

[ApiController]
[Route("api/BloggingPlatform")]
public class BloggingPlatformController : Controller
{
    #region private fields
    private readonly IApplicationService _service;
    private readonly ILogger<BloggingPlatformController> _logger;
    #endregion

    #region constructor
    public BloggingPlatformController(IApplicationService service, ILogger<BloggingPlatformController> logger)
    {
        #region Initialize checks
        InitializeChecks.InitialCheck(service, "ApplicationService cannot be null");
        InitializeChecks.InitialCheck(logger, "Logger cannot be null");
        #endregion

        _service = service;
        _logger = logger;
    }
    #endregion

    #region Users

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddUser(UserDto user)
    {
        try
        {
            _service.AddUser(user);
            _logger.LogInformation("Blogging Platform Controller - Add user call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("Blogging Platform Controller - Add user call not executed. {exMessage}", ex.Message);
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateUser(UserDto user)
    {
        try
        {
            _service.UpdateUser(user);
            _logger.LogInformation("Blogging Platform Controller - Update user call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("Blogging Platform Controller - Update user call not executed. {exMessage}", ex.Message);
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("[action]/{userGuid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteUser(string userGuid)
    {
        try
        {
            _service.DeleteUser(Guid.Parse(userGuid));
            _logger.LogInformation("Blogging Platform Controller - Delete user call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("Blogging Platform Controller - Delete user call not executed. {exMessage}", ex.Message);
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]/{userGuid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetUserByGuid(string userGuid)
    {
        try
        {
            UserDto user = _service.GetUserByGuid(Guid.Parse(userGuid));
            user.Salt = "";
            user.HashCode = "";
            _logger.LogInformation("Blogging Platform Controller - Get user by guid call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError("Blogging Platform Controller - Get user by guid call not executed. {exMessage}", ex.Message);
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllUsers()
    {
        try
        {
            List<UserDto> users = _service.GetAllUsers();
            foreach(UserDto user in users)
            {
                user.Salt = "";
                user.HashCode = "";
            }
            _logger.LogInformation("Blogging Platform Controller - Get all users call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError("Blogging Platform Controller - Get all users call not executed. {exMessage}", ex.Message);
            return BadRequest();
        }
    }

    #endregion

    #region BlogPosts

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddBlogPost(BlogPostDto blogPost)
    {
        try
        {
            _service.AddBlogPost(blogPost);
            _logger.LogInformation("Blogging Platform Controller - Add blog post call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("Blogging Platform Controller - Add blog post call not executed. {exMessage}", ex.Message);
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateBlogPost(BlogPostDto blogPost)
    {
        try
        {
            _service.UpdateBlogPost(blogPost);
            _logger.LogInformation("Blogging Platform Controller - Update blog post call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("Blogging Platform Controller - Update blog post call not executed. {exMessage}", ex.Message);
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("[action]/{blogPostGuid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteBlogPost(string blogPostGuid)
    {
        try
        {
            _service.DeleteBlogPost(Guid.Parse(blogPostGuid));
            _logger.LogInformation("Blogging Platform Controller - Delete blog post call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("Blogging Platform Controller - Delete blog post call not executed. {exMessage}", ex.Message);
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]/{blogPostGuid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetBlogPostByGuid(string blogPostGuid)
    {
        try
        {
            BlogPostDto blogPost = _service.GetBlogPostByGuid(Guid.Parse(blogPostGuid));
            _logger.LogInformation("Blogging Platform Controller - Get blog post by guid call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok(blogPost);
        }
        catch (Exception ex)
        {
            _logger.LogError("Blogging Platform Controller - Get blog post by guid call not executed. {exMessage}", ex.Message);
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllBlogPosts()
    {
        try
        {
            List<BlogPostDto> blogPosts = _service.GetAllBlogPosts();
            _logger.LogInformation("Blogging Platform Controller - Get all blog posts call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok(blogPosts);
        }
        catch (Exception ex)
        {
            _logger.LogError("Blogging Platform Controller - Get all blog posts call not executed. {exMessage}", ex.Message);
            return BadRequest();
        }
    }

    #endregion
}
