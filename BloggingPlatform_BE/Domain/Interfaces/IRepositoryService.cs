using BloggingPlatform_BE.Application.DTOs;

namespace BloggingPlatform_BE.Domain.Interfaces;

public interface IRepositoryService
{
    /// <summary>
    /// Create database and tables if not exist
    /// </summary>
    /// <param name="directory">directory of db and .sql file</param>
    /// <param name="filePath">file path for db creation</param>
    public void InitializeTables(string directory, string filePath);

    /// <summary>
    /// Adding user to the repository
    /// </summary>
    /// <param name="user">user object to add to the db</param>
    public void AddUser(UserDto user);

    /// <summary>
    /// Updating user to the repository
    /// </summary>
    /// <param name="user">user object to add to the db</param>
    public void UpdateUser(UserDto user);

    /// <summary>
    /// Deleting user to the repository 
    /// </summary>
    /// <param name="userGuid">guid of the user that has to be deleted</param>
    public void DeleteUser(Guid userGuid);

    /// <summary>
    /// Getting user by a passed guid
    /// </summary>
    /// <param name="userGuid">guid of the user returned</param>
    /// <returns>returns the user with a specific guid</returns>
    public UserDto GetUserByGuid(Guid userGuid);
    
    /// <summary>
    /// Get all users from db
    /// </summary>
    /// <returns>returns a list of all users in db</returns>
    public List<UserDto> GetAllUsers();


    /// <summary>
    /// Adding blogPost to the repository
    /// </summary>
    /// <param name="blogPost">blogPost object to add to the db</param>
    public void AddBlogPost(BlogPostDto blogPost);
    
    /// <summary>
    /// Updating blogPost to the db
    /// </summary>
    /// <param name="blogPost">blogPost object to add to the db</param>
    public void UpdateBlogPost(BlogPostDto blogPost);

    /// <summary>
    /// Deleting blogPost to the db
    /// </summary>
    /// <param name="blogPostGuid">the guid of the blogPost that has to be deleted</param>
    public void DeleteBlogPost(Guid blogPostGuid);

    /// <summary>
    /// Getting a blogPost from a passed guid
    /// </summary>
    /// <param name="blogPostGuid">the guid of the blogPost that will be returned</param>
    /// <returns>the blogPost with the passed guid</returns>
    public BlogPostDto GetBlogPostByGuid(Guid blogPostGuid);

    /// <summary>
    /// Get all blogPosts from db
    /// </summary>
    /// <returns>returns all blogPosts</returns>
    public List<BlogPostDto> GetAllBlogPosts();
}
