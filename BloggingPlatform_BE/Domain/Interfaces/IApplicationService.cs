using BloggingPlatform_BE.Application.DTOs;

namespace BloggingPlatform_BE.Domain.Interfaces;

public interface IApplicationService
{
    /// <summary>
    /// Create a casual salt, create a hashcode from the salt and the password.
    /// Calls the AddUser repository method.
    /// </summary>
    /// <param name="user">user object from controller</param>
    /// <exception cref="Exception">catch exception from repository and from authentication service</exception>
    /// <exception>throw a new exception with more info</exception>
    public void AddUser(UserDto user);

    /// <summary>
    /// Check if a user with the passed guid exists, if not, exception
    /// if yes, check if password has changed, if yes, calculate new salt and hashcode
    /// if not, take the salt and the hash from db and send the user to the repository service
    /// </summary>
    /// <param name="user">user object from controller</param>
    /// <exception cref="Exception">catch exception from repository and from authentication service</exception>
    /// <exception>throw a new exception with more info</exception>
    public void UpdateUser(UserDto user);

    /// <summary>
    /// Delete a user with the passed user guid from the repository
    /// </summary>
    /// <param name="userGuid">the guid of the user that has to be deleted</param>
    /// <exception cref="Exception">catch the exception from the repository service</exception>
    /// <exception>throw a new exception with more info</exception>
    public void DeleteUser(Guid userGuid);

    /// <summary>
    /// Checks if user with user guid exists, if yes, return it, if not, exception
    /// </summary>
    /// <param name="userGuid">the guid of the user that has to be returned</param>
    /// <exception cref="Exception">catch the exception from the repository service</exception>
    /// <exception>throw a new exception with more info</exception>
    public UserDto GetUserByGuid(Guid userGuid);

    /// <summary>
    /// Returns all users in repository
    /// </summary>
    /// <exception cref="Exception">catch the exception from the repository service</exception>
    /// <exception>throw a new exception with more info</exception>
    public List<UserDto> GetAllUsers();


    /// <summary>
    /// Calls the AddBlogPost repository method (add a blog post to db)
    /// </summary>
    /// <param name="blogPost">blogPost object from controller</param>
    /// <exception cref="Exception">catch exception from repository</exception>
    /// <exception>throw a new exception with more info</exception>
    public void AddBlogPost(BlogPostDto blogPost);

    /// <summary>
    /// Calls the UpdateBlogPost repository method (update a blog post in db)
    /// </summary>
    /// <param name="blogPost">blogPost object from controller</param>
    /// <exception cref="Exception">catch exception from repository</exception>
    /// <exception>throw a new exception with more info</exception>
    public void UpdateBlogPost(BlogPostDto blogPost);

    /// <summary>
    /// Calls the DeleteBlogPost repository method (delete the blog post with the passed guid)
    /// </summary>
    /// <param name="blogPostGuid">guid of the blog post that has to be deleted</param>
    /// <exception cref="Exception">catch exception from repository</exception>
    /// <exception>throw a new exception with more info</exception>
    public void DeleteBlogPost(Guid blogPostGuid);

    /// <summary>
    /// Calls the GetBlogPostByGuid method from repository, return the searched blog post if exists
    /// </summary>
    /// <param name="blogPostGuid">guid of the blog post that has to be returned</param>
    /// <exception cref="Exception">catch exception from repository</exception>
    /// <exception>throw a new exception with more info</exception>
    public BlogPostDto GetBlogPostByGuid(Guid blogPostGuid);

    /// <summary>
    /// Calls the GetAllBlogPosts method from repository, return all blogPosts
    /// </summary>
    /// <exception cref="Exception">catch exception from repository</exception>
    /// <exception>throw a new exception with more info</exception>
    public List<BlogPostDto> GetAllBlogPosts();

    /// <summary>
    /// Calls the InitializeTables method from repository
    /// </summary>
    public void CreateDataBase();
}
