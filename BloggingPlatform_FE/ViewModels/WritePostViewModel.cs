using BloggingPlatform_FE.Models;
using BloggingPlatform_FE.Interfaces;
using LusiUtilsLibrary.Backend.Initialization;
using LusiUtilsLibrary.Frontend.MVVMHelpers;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Windows.Input;

namespace BloggingPlatform_FE.ViewModels;

public class WritePostViewModel : INotifyPropertyChanged
{
    private readonly IRequestService_FE _requestService;
    private readonly INavigationService _navigationService;
    private readonly IMemoryService _memoryService;
    private readonly ILogger<WritePostViewModel> _logger;

    private string _postTitle;
    private string _postContent;
    private string _postTags;
    private string _errorText;

    public WritePostViewModel(IRequestService_FE requestService, INavigationService navigationService, IMemoryService memoryService, ILogger<WritePostViewModel> logger)
    {
        InitializeChecks.InitialCheck(requestService, "Request Service cannot be null");
        InitializeChecks.InitialCheck(navigationService, "Navigation Service cannot be null");
        InitializeChecks.InitialCheck(memoryService, "Memory Service cannot be null");
        InitializeChecks.InitialCheck(logger, "Logger cannot be null");

        _requestService = requestService;
        _navigationService = navigationService;
        _memoryService = memoryService;
        _logger = logger;

        HomeButton = new RelayCommand(async () => await GoToHome());
        WritePostButton = new RelayCommand(async () => await GoToWritePost());
        SeeMyPostButton = new RelayCommand(async () => await GoToSeeMyPosts());
        ExitButton = new RelayCommand(async () => await Exit());
        InsertPostButton = new RelayCommand(async () => await Insert());
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
    public ICommand InsertPostButton { get; }

    private async Task GoToHome() => _navigationService.NavigateTo("Home");

    private async Task GoToWritePost() => _navigationService.NavigateTo("WritePost");

    private async Task GoToSeeMyPosts() => _navigationService.NavigateTo("PersonalPosts");

    private async Task Exit() => App.Current.Shutdown();

    
    private async Task Insert()
    {
        int currentUserId = _memoryService.GetCurrentUser().UserId;

        if(_postTitle == string.Empty && _postContent == string.Empty && _postTags == string.Empty)
        {
            _logger.LogError("WritePostViewModel - Title, content or tags are empty");
            ErrorText = "Post not added - title, content or tags are empty";
            return;
        }


        BlogPostDto newBlogPost = new BlogPostDto()
        {
            UserId = currentUserId,
            PostGuid = Guid.NewGuid(),
            PostTitle = _postTitle,
            PostContent = _postContent,
            PostTags = _postTags,
            PostCreatedOn = DateTime.UtcNow
        };

        _requestService.AddBlogPost(newBlogPost);

        _logger.LogInformation("WritePostViewModel - blog post added succesfully, navigating to home page");

        _navigationService.NavigateTo("Home");
    }

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}
