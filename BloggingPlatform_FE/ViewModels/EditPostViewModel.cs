using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Models;
using LusiUtilsLibrary.Backend.Initialization;
using LusiUtilsLibrary.Frontend.MVVMHelpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BloggingPlatform_FE.ViewModels;

public class EditPostViewModel
{
    private readonly IRequestService_FE _requestService;
    private readonly INavigationService _navigationService;
    private readonly IMemoryService _memoryService;
    private readonly ILogger<EditPostViewModel> _logger;

    private string _postTitle;
    private string _postContent;
    private string _postTags;
    private string _errorText;
    private BlogPostDto _currentPost;

    public EditPostViewModel(IRequestService_FE requestService, INavigationService navigationService, IMemoryService memoryService, ILogger<EditPostViewModel> logger)
    {
        InitializeChecks.InitialCheck(requestService, "Request Service cannot be null");
        InitializeChecks.InitialCheck(navigationService, "Navigation Service cannot be null");
        InitializeChecks.InitialCheck(memoryService, "Memory Service cannot be null");
        InitializeChecks.InitialCheck(logger, "Logger cannot be null");

        _requestService = requestService;
        _navigationService = navigationService;
        _memoryService = memoryService;
        _logger = logger;

        HomeButton = new RelayCommand(GoToHome);
        WritePostButton = new RelayCommand(GoToWritePost);
        SeeMyPostButton = new RelayCommand(GoToSeeMyPosts);
        ExitButton = new RelayCommand(Exit);
        EditPostButton = new RelayCommand(Edit);

        _currentPost = GetCurrentPost();

        PostTitle = _currentPost.PostTitle;
        PostContent = _currentPost.PostContent;
        PostTags = _currentPost.PostTags;
    }

    public string PostTitle
    {
        get => _postTitle;
        set
        {
            _postTitle = value;
            OnPropertyChanged(nameof(PostTitle));
        }
    }
    public string PostContent
    {
        get => _postContent;
        set
        {
            _postContent = value;
            OnPropertyChanged(nameof(PostContent));
        }
    }
    public string PostTags
    {
        get => _postTags;
        set
        {
            _postTags = value;
            OnPropertyChanged(nameof(PostTags));
        }
    }

    public string ErrorText
    {
        get => _errorText;
        set
        {
            _errorText = value;
            OnPropertyChanged(nameof(ErrorText));
        }
    }

    public ICommand HomeButton { get; }
    public ICommand WritePostButton { get; }
    public ICommand SeeMyPostButton { get; }
    public ICommand ExitButton { get; }
    public ICommand EditPostButton { get; }

    private async Task GoToHome() => _navigationService.NavigateTo("Home");

    private async Task GoToWritePost() => _navigationService.NavigateTo("WritePost");

    private async Task GoToSeeMyPosts() => _navigationService.NavigateTo("PersonalPosts");

    private async Task Exit() => App.Current.Shutdown();


    private async Task Edit()
    {
        if (_postTitle == string.Empty && _postContent == string.Empty && _postTags == string.Empty)
        {
            _logger.LogError("EditPostViewModel - Title, content or tags are empty");
            ErrorText = "Post not updated - title, content or tags are empty";
            return;
        }


        BlogPostDto newBlogPost = new BlogPostDto()
        {
            PostGuid = _currentPost.PostGuid,
            PostTitle = _postTitle,
            PostContent = _postContent,
            PostTags = _postTags,
            PostCreatedOn = _currentPost.PostCreatedOn,
            PostModifiedOn = DateTime.UtcNow,
        };

        await _requestService.UpdateBlogPost(newBlogPost);

        _logger.LogInformation("EditPostViewModel - blog post updated succesfully, navigating to home page");

        _navigationService.NavigateTo("PersonalPosts");
    }

    private BlogPostDto GetCurrentPost()
    {
        return _memoryService.GetCurrentPost();
    }

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion

}
