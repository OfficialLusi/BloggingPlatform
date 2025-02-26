using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Models;
using LusiUtilsLibrary.Backend.APIs_REST;
using LusiUtilsLibrary.Backend.Initialization;
using Microsoft.Extensions.Logging;

namespace BloggingPlatform_FE.Services;

public class RequestService_FE : IRequestService_FE
{
    private readonly ILogger<IRequestService_FE> _logger;
    private readonly IREST_RequestService _service;

    public RequestService_FE(ILogger<IRequestService_FE> logger, IREST_RequestService service)
    {
        #region Initialize
        InitializeChecks.InitialCheck(logger, "Logger cannot be null");
        InitializeChecks.InitialCheck(service, "REST_RequestService cannot be null");
        #endregion

        _logger = logger;
        _service = service;
    }

    #region User

    public async Task<ApiResponse<UserDto>> AddUser(UserDto user)
    {
        try
        {
            ApiResponse<UserDto> data = await _service.ExecuteRequestAsync<UserDto>("AddUser", RequestType.POST, user);
            _logger.LogInformation("RequestService_FE - Add user call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Add user call not executed.", ex);
        }
    }

    public async Task<ApiResponse<UserDto>> UpdateUser(UserDto user)
    {
        try
        {
            ApiResponse<UserDto> data = await _service.ExecuteRequestAsync<UserDto>("UpdateUser", RequestType.PUT, user);
            _logger.LogInformation("RequestService_FE - Update user call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Update user call not executed.", ex);
        }
    }

    public async Task DeleteUser(Guid userGuid)
    {
        try
        {
            Dictionary<string, string> param = new()
            {
                { "userGuid" , userGuid.ToString() }
            };

            await _service.ExecuteRequestAsync<UserDto>("DeleteUser", RequestType.DELETE, null, param);
            _logger.LogInformation("RequestService_FE - Delete user call executed correctly");
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Delete user call not executed.", ex);
        }
    }

    public async Task<ApiResponse<UserDto>> GetUserByGuid(Guid userGuid)
    {
        try
        {
            Dictionary<string, string> param = new()
            {
                { "userGuid" , userGuid.ToString() }
            };

            ApiResponse<UserDto> data = await _service.ExecuteRequestAsync<UserDto>("GetUserByGuid", RequestType.GET, null, param);
            _logger.LogInformation("RequestService_FE - Get user by guid call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Get user by guid call not executed.", ex);
        }
    }

    public async Task<ApiResponse<List<UserDto>>> GetAllUsers()
    {
        try
        {
            ApiResponse<List<UserDto>> data = await _service.ExecuteRequestAsync<List<UserDto>>("GetAllUsers", RequestType.GET, null);
            _logger.LogInformation("RequestService_FE - Get all users call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Get all users call not executed.", ex);
        }
    }

    #endregion

    #region BlogPost

    public async Task<ApiResponse<BlogPostDto>> AddBlogPost(BlogPostDto blogPost)
    {
        try
        {
            ApiResponse<BlogPostDto> data = await _service.ExecuteRequestAsync<BlogPostDto>("AddBlogPost", RequestType.POST, blogPost);
            _logger.LogInformation("RequestService_FE - Add blog post call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Add blog post call not executed.", ex);
        }
    }

    public async Task<ApiResponse<BlogPostDto>> UpdateBlogPost(BlogPostDto blogPost)
    {
        try
        {
            ApiResponse<BlogPostDto> data = await _service.ExecuteRequestAsync<BlogPostDto>("UpdateBlogPost", RequestType.PUT, blogPost);
            _logger.LogInformation("RequestService_FE - Update blog post call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Update blog post call not executed.", ex);
        }
    }

    public async Task DeleteBlogPost(Guid blogPostGuid)
    {
        try
        {
            Dictionary<string, string> param = new()
            {
                {"blogPostGuid", blogPostGuid.ToString()} 
            };

            string[] args = [blogPostGuid.ToString()];
            await _service.ExecuteRequestAsync<BlogPostDto>("DeleteBlogPost", RequestType.DELETE, null, param);
            _logger.LogInformation("RequestService_FE - Delete blog post call executed correctly");
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Delete blog post call not executed.", ex);
        }
    }

    public async Task<ApiResponse<BlogPostDto>> GetBlogPostByGuid(Guid blogPostGuid)
    {
        try
        {
            Dictionary<string, string> param = new()
            {
                {"blogPostGuid", blogPostGuid.ToString()}
            };

            ApiResponse<BlogPostDto> data = await _service.ExecuteRequestAsync<BlogPostDto>("GetBlogPostByGuid", RequestType.GET, null, param);
            _logger.LogInformation("RequestService_FE - Get blog post by guid call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Get blog post by guid call not executed.", ex);
        }
    }

    public async Task<ApiResponse<List<BlogPostDto>>> GetAllBlogPosts()
    {
        try
        {
            ApiResponse<List<BlogPostDto>> data = await _service.ExecuteRequestAsync<List<BlogPostDto>>("GetAllBlogPosts", RequestType.GET, null);
            _logger.LogInformation("RequestService_FE - Get all blog posts call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Get all blog posts call not executed.", ex);
        }
    }
    #endregion

    public async Task<ApiResponse<UserDto>> AuthenticateUser(UserDto user)
    {
        try
        {
            ApiResponse<UserDto> data = await _service.ExecuteRequestAsync<UserDto>("AuthenticateUser", RequestType.POST, user);
            _logger.LogInformation("RequestService_FE - Authenticate user call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Authenticate user call not executed.", ex);
        }
    }
}
