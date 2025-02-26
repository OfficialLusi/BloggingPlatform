using System.ComponentModel;
using System.Net;
using System.Windows.Controls;
using System.Windows.Input;
using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Models;
using LusiUtilsLibrary.Backend.APIs_REST;
using LusiUtilsLibrary.Backend.Initialization;
using LusiUtilsLibrary.Frontend.MVVMHelpers;

namespace BloggingPlatform_FE.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _userInfo;
    private string _wrongCredentials;

    private readonly IRequestService_FE _requestService;
    private readonly INavigationService _navigationService;
    private readonly IMemoryService _memoryService;


    public LoginViewModel(IRequestService_FE requestService, INavigationService navigationService, IMemoryService memoryService)
    {
        #region initialize checks
        InitializeChecks.InitialCheck(requestService, "Request service cannot be null");
        InitializeChecks.InitialCheck(navigationService, "Navigation service cannot be null");
        InitializeChecks.InitialCheck(memoryService, "Memory service cannot be null");
        #endregion

        _requestService = requestService;
        _navigationService = navigationService;
        _memoryService = memoryService;

        LoginCommand = new RelayCommand<object>(async (param) => await UserLogin(param), (param) => true);
        NavigateToSignupCommand = new RelayCommand(() => UserSignup());

        if (!_memoryService.GetFirstLogin())
        {
            WrongCredentials = "Wrong Credentials";
            OnPropertyChanged(nameof(WrongCredentials));
        }
    }

    public string UserInfo
    {
        get => _userInfo;
        set
        {
            _userInfo = value;
            OnPropertyChanged(nameof(UserInfo));
        }
    }
    public string WrongCredentials
    {
        get => _wrongCredentials;
        set
        {
            _wrongCredentials = value;
            OnPropertyChanged(nameof(WrongCredentials));
        }
    }

    public ICommand LoginCommand { get; }
    public ICommand NavigateToSignupCommand { get; }

    #region private methods

    public async Task UserLogin(object parameter)
    {
        UserDto user = new UserDto();

        if (parameter is PasswordBox pb)
            user.UserPassword = pb.Password;

        if (_userInfo.Contains('@'))
            user.UserEmail = _userInfo;
        else
            user.UserName = _userInfo;

        ApiResponse<UserDto> data = await _requestService.AuthenticateUser(user);

        if (data.StatusCode == HttpStatusCode.OK)
        {
            _navigationService.NavigateTo("Home");
            _memoryService.SetCurrentUser(data.Data);
        }
        else
        {
            _memoryService.SetFirstLoginFalse();
            _navigationService.NavigateTo("Login");
        }
    }


    public async Task UserSignup()
    {
        _navigationService.NavigateTo("Signup");
    }

    #endregion


    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}
