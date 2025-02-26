using BloggingPlatform_FE.Models;
using LusiUtilsLibrary.Backend.APIs_REST;

namespace BloggingPlatform_FE.Interfaces;

public interface IRequestService_FE
{
    #region User

    /// <summary>
    /// Execute request to add a user to the database
    /// </summary>
    /// <param name="user">user object to add to the db</param>
    /// <returns>class with returned object (if present) and status code</returns>
    /// <exception cref="Exception">catch exception if call fail and rethrow it</exception>
    public Task<ApiResponse<UserDto>> AddUser(UserDto user);

    /// <summary>
    /// Execute request to update a user to the database
    /// </summary>
    /// <param name="user">user object to add to the db</param>
    /// <returns>class with returned object (if present) and status code</returns>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
    public Task<ApiResponse<UserDto>> UpdateUser(UserDto user);

    /// <summary>
    /// Execute request to delete a user from the database
    /// </summary>
    /// <param name="userGuid">guid of the user that has to be deleted</param>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
    public Task DeleteUser(Guid userGuid);

    /// <summary>
    /// Execute request to get a user from his guid
    /// </summary>
    /// <param name="userGuid">guid of the user that has to be returned</param>
    /// <returns>user object and status code of the request</returns>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
    public Task<ApiResponse<UserDto>> GetUserByGuid(Guid userGuid);

    /// <summary>
    /// Execute request to get all users
    /// </summary>
    /// <returns>list of user object and status code of the request</returns>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
    public Task<ApiResponse<List<UserDto>>> GetAllUsers();

    #endregion

    #region BlogPost

    /// <summary>
    /// Executes request to add a blog post to the database
    /// </summary>
    /// <param name="blogPost">blog post object to add to the database</param>
    /// <returns>blogPost object (if returned) and status code of the request</returns>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
    public Task<ApiResponse<BlogPostDto>> AddBlogPost(BlogPostDto blogPost);

    /// <summary>
    /// Execute request to update a blog post from the database
    /// </summary>
    /// <param name="blogPost">blog post object to update</param>
    /// <returns>blog post object updated and status code of the request</returns>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
    public Task<ApiResponse<BlogPostDto>> UpdateBlogPost(BlogPostDto blogPost);

    /// <summary>
    /// Execute request to delete a blog post from the database
    /// </summary>
    /// <param name="blogPostGuid">guid of the user to delete</param>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
    public Task DeleteBlogPost(Guid blogPostGuid);

    /// <summary>
    /// Execute request to get a blog post from a guid
    /// </summary>
    /// <param name="blogPostGuid">guid of the blog post that has to be returned</param>
    /// <returns>blog post object and status code of the request</returns>
    /// <exception cref="Exception">catch exception if the call fails and rethrows it</exception>
    public Task<ApiResponse<BlogPostDto>> GetBlogPostByGuid(Guid blogPostGuid);

    /// <summary>
    /// Execute request to get all blog posts
    /// </summary>
    /// <returns>a list of blog post objects and status code of the request</returns>
    /// <exception cref="Exception">catch exception if call fails and rethrow it</exception>
    public Task<ApiResponse<List<BlogPostDto>>> GetAllBlogPosts();
    #endregion

    /// <summary>
    /// Execute request to authenticate user
    /// </summary>
    /// <param name="user">user object to authenticate</param>
    /// <returns>full user object and status code of the request</returns>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
    public Task<ApiResponse<UserDto>> AuthenticateUser(UserDto user);
}
