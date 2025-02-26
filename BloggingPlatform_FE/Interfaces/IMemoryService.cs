using BloggingPlatform_FE.Models;

namespace BloggingPlatform_FE.Interfaces;

public interface IMemoryService
{
    /// <summary>
    /// Getting the state of the current user logged
    /// </summary>
    /// <returns></returns>
    public UserDto GetCurrentUser();
    /// <summary>
    /// Setting the state of the current user logged
    /// </summary>
    /// <param name="user">the user to use</param>
    public void SetCurrentUser(UserDto user);

    /// <summary>
    /// Getting the current post (edit page)
    /// </summary>
    /// <returns>the blog post instance to edit</returns>
    public BlogPostDto GetCurrentPost();
    /// <summary>
    /// set the current post to edit
    /// </summary>
    /// <param name="post">the post to edit</param>
    public void SetCurrentPost(BlogPostDto post);

    /// <summary>
    /// Set if it is the first login or not
    /// </summary>
    public void SetFirstLoginFalse();
    /// <summary>
    /// Get if it is the first login or not
    /// </summary>
    /// <returns></returns>
    public bool GetFirstLogin();


}
