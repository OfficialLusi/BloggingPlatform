using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Models;
using BloggingPlatform_FE.Services;
using BloggingPlatform_FE.Views;
using LusiUtilsLibrary.Backend.APIs_REST;
using LusiUtilsLibrary.Backend.Initialization;
using LusiUtilsLibrary.Frontend.MVVMHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows.Input;

namespace BloggingPlatform_FE.ViewModels;

public class PersonalPostViewModel : INotifyPropertyChanged
{
    private readonly IRequestService_FE _requestService;
    private readonly INavigationService _navigationService;
    private readonly IMemoryService _memoryService;
    private readonly CompareService _compareService;
    private readonly ILogger<PersonalPostViewModel> _logger;

    private string _searchedWord;

    public PersonalPostViewModel(IRequestService_FE requestService, INavigationService navigationService, IMemoryService memoryService, CompareService compareService, ILogger<PersonalPostViewModel> logger)
    {
        InitializeChecks.InitialCheck(requestService, "Request Service cannot be null");
        InitializeChecks.InitialCheck(navigationService, "Navigation Service cannot be null");
        InitializeChecks.InitialCheck(memoryService, "Memory Service cannot be null");
        InitializeChecks.InitialCheck(compareService, "Compare Service cannot be null");
        InitializeChecks.InitialCheck(logger, "Logger cannot be null");

        _requestService = requestService;
        _navigationService = navigationService;
        _memoryService = memoryService;
        _compareService = compareService;
        _logger = logger;

        HomeButton = new RelayCommand(GoToHome);
        WritePostButton = new RelayCommand(GoToWritePost);
        SeeMyPostButton = new RelayCommand(GoToSeeMyPosts);
        ExitButton = new RelayCommand(Exit);
        SearchButton = new RelayCommand(Search);

        EditButton = new RelayCommand<object>(Edit);
        DeleteButton = new RelayCommand<object>(Delete);

        ShowMyAllPosts();
    }

    public string SearchedWord
    {
        get => _searchedWord;
        set
        {
            _searchedWord = value;
            OnPropertyChanged(nameof(SearchedWord));
        }
    }

    public ICommand HomeButton { get; }
    public ICommand WritePostButton { get; }
    public ICommand SeeMyPostButton { get; }
    public ICommand ExitButton { get; }
    public ICommand SearchButton { get; }
    public ICommand EditButton { get; }
    public ICommand DeleteButton { get; }

    public ObservableCollection<BlogPostDto> BlogPosts { get; set; } = new ObservableCollection<BlogPostDto>();
    private List<BlogPostDto> SortedBlogPosts { get; set; } = new List<BlogPostDto>();


    private async Task GoToHome() => _navigationService.NavigateTo("Home");

    private async Task GoToWritePost() => _navigationService.NavigateTo("WritePost");

    private async Task GoToSeeMyPosts() => _navigationService.NavigateTo("PersonalPosts");

    private async Task Exit() => App.Current.Shutdown();

    private async Task Search()
    {
        if (string.IsNullOrEmpty(_searchedWord))
            return;

        BlogPosts.Clear();
        SortedBlogPosts.Clear();

        int currentUserId = _memoryService.GetCurrentUser().UserId;

        ApiResponse<List<BlogPostDto>> data = await _requestService.GetAllBlogPosts();

        List<BlogPostDto> userPosts = data.Data.Where(x => x.UserId == currentUserId).ToList();

        foreach (BlogPostDto blogPostDto in userPosts)
        {
            if (blogPostDto.PostTags.Contains(_searchedWord, StringComparison.InvariantCultureIgnoreCase) || blogPostDto.PostTitle.Contains(_searchedWord, StringComparison.InvariantCultureIgnoreCase))
                SortedBlogPosts.Add(blogPostDto);
        }

        SortedBlogPosts.Sort(_compareService);
        BlogPosts = new(SortedBlogPosts);
        OnPropertyChanged(nameof(BlogPosts));
    }

    private async Task ShowMyAllPosts()
    {
        int currentUserId = _memoryService.GetCurrentUser().UserId;
        BlogPosts.Clear();
        SortedBlogPosts.Clear();

        ApiResponse<List<BlogPostDto>> data = await _requestService.GetAllBlogPosts();

        if (data.StatusCode == System.Net.HttpStatusCode.OK && data.Data != null)
        {
            _logger.LogInformation("PersonalPostViewModel - Correctly received list of all blog posts");
            foreach (var post in data.Data)
            {
                if (post.UserId == currentUserId)
                    SortedBlogPosts.Add(post);
            }

            SortedBlogPosts.Sort(_compareService);
            BlogPosts = new(SortedBlogPosts);
            OnPropertyChanged(nameof(BlogPosts));
        }
    }

    private async Task Edit(object param)
    {
        var navigationService = App.ServiceProvider.GetRequiredService<INavigationService>();

        if (param is BlogPostDto blogPostToEdit)
        {
            _memoryService.SetCurrentPost(blogPostToEdit);
            navigationService.NavigateTo("EditPost");
        }
    }

    private async Task Delete(object param)
    {
        var navigationService = App.ServiceProvider.GetRequiredService<INavigationService>();

        if (param is BlogPostDto blogPostToDelete)
        {
            var dialog = new DeleteConfirmationDialogView(new DeleteConfirmationDialogViewModel());

            // getting the result of the login signup dialog
            bool? result = dialog.ShowDialog();


            if (result.HasValue && result.Value)
            {
                // getting the navigation service from the di container

                if (dialog.ChosenOption == "Yes")
                    await _requestService.DeleteBlogPost(blogPostToDelete.PostGuid);
            }

        }
        navigationService.NavigateTo("PersonalPosts");
    }

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}