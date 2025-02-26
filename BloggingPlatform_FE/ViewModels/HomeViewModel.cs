using BloggingPlatform_FE.Models;
using BloggingPlatform_FE.Interfaces;
using LusiUtilsLibrary.Backend.APIs_REST;
using LusiUtilsLibrary.Backend.Initialization;
using LusiUtilsLibrary.Frontend.MVVMHelpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using BloggingPlatform_FE.Services;
using System.Windows.Controls;

namespace BloggingPlatform_FE.ViewModels;

public class HomeViewModel : INotifyPropertyChanged
{
    private readonly IRequestService_FE _requestService;
    private readonly INavigationService _navigationService;
    private readonly CompareService _compareService;
    private readonly ILogger<HomeViewModel> _logger;

    private string _searchedWord;

    public HomeViewModel(IRequestService_FE requestService, INavigationService navigationService, CompareService compareService, ILogger<HomeViewModel> logger)
    {
        InitializeChecks.InitialCheck(requestService, "Request Service cannot be null");
        InitializeChecks.InitialCheck(navigationService, "Navigation Service cannot be null");
        InitializeChecks.InitialCheck(compareService, "Compare Service cannot be null");
        InitializeChecks.InitialCheck(logger, "Logger cannot be null");

        _requestService = requestService;
        _navigationService = navigationService;
        _compareService = compareService;
        _logger = logger;

        HomeButton = new RelayCommand(GoToHome);
        WritePostButton = new RelayCommand(GoToWritePost);
        SeeMyPostButton = new RelayCommand(GoToSeeMyPosts);
        ExitButton = new RelayCommand(Exit);
        SearchButton = new RelayCommand(Search);

        ShowAllPosts();
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

    public ObservableCollection<BlogPostDto> BlogPosts { get; set; } = new ObservableCollection<BlogPostDto>();
    private List<BlogPostDto> SortedBlogPosts { get; set; } = new List<BlogPostDto>();


    private async Task GoToHome() => _navigationService.NavigateTo("Home");

    private async Task GoToWritePost() => _navigationService.NavigateTo("WritePost");

    private async Task GoToSeeMyPosts() => _navigationService.NavigateTo("PersonalPosts");

    private async Task Exit() => App.Current.Shutdown();

    private async Task Search()
    {
        if(string.IsNullOrEmpty(_searchedWord))
            return;

        BlogPosts.Clear();
        SortedBlogPosts.Clear();

        ApiResponse<List<BlogPostDto>> data = await _requestService.GetAllBlogPosts();

        foreach (BlogPostDto blogPostDto in data.Data)
        {
            if (blogPostDto.PostTags.Contains(_searchedWord, StringComparison.InvariantCultureIgnoreCase) || blogPostDto.PostTitle.Contains(_searchedWord, StringComparison.InvariantCultureIgnoreCase))
                SortedBlogPosts.Add(blogPostDto);
        }

        // sort by the date (showing the recent ones) 
        SortedBlogPosts.Sort(_compareService);
        BlogPosts = new(SortedBlogPosts);
        OnPropertyChanged(nameof(BlogPosts));
    }

    private async Task ShowAllPosts()
    {
        ApiResponse<List<BlogPostDto>> data = await _requestService.GetAllBlogPosts();

        if (data.StatusCode == System.Net.HttpStatusCode.OK && data.Data != null)
        {
            _logger.LogInformation("HomeViewModel - Correctly received list of all blog posts");
            BlogPosts.Clear();
            SortedBlogPosts.Clear();
            foreach (var post in data.Data)
            {
                SortedBlogPosts.Add(post);
            }

            // sort by the date (showing the recent ones) 
            SortedBlogPosts.Sort(_compareService);
            BlogPosts = new(SortedBlogPosts);
            OnPropertyChanged(nameof(BlogPosts));
        }
    }

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}
