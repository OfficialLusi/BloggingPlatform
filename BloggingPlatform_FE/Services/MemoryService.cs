using BloggingPlatform_FE.Models;
using BloggingPlatform_FE.Interfaces;

namespace BloggingPlatform_FE.Services;

public class MemoryService : IMemoryService
{
    private UserDto _currentUser;
    private BlogPostDto _blogPost;
    private bool _firstLogin = true;

    public UserDto GetCurrentUser() => _currentUser;
    public void SetCurrentUser(UserDto user) => _currentUser = user;

    public BlogPostDto GetCurrentPost() => _blogPost;
    public void SetCurrentPost(BlogPostDto post) => _blogPost = post;

    public void SetFirstLoginFalse() =>_firstLogin = false;
    public bool GetFirstLogin() => _firstLogin;
}
