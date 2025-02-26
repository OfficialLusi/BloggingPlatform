using BloggingPlatform_FE.Models;
using BloggingPlatform_FE.Interfaces;
using LusiUtilsLibrary.Backend.Initialization;
using LusiUtilsLibrary.Frontend.MVVMHelpers;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using LusiUtilsLibrary.Backend.APIs_REST;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

namespace BloggingPlatform_FE.ViewModels
{
    public class SignupViewModel
    {
        private readonly IRequestService_FE _requestService;
        private readonly INavigationService _navigationService;
        private readonly ILogger<SignupViewModel> _logger;

        private string userNameParam;
        private string userSurnameParam;
        private string userEmailParam;


        public SignupViewModel(IRequestService_FE requestService, INavigationService navigationService, ILogger<SignupViewModel> logger)
        {
            #region initialize checks
            InitializeChecks.InitialCheck(requestService, "Request service cannot be null");
            InitializeChecks.InitialCheck(navigationService, "Navigation service cannot be null");
            InitializeChecks.InitialCheck(logger, "Logger cannot be null");
            #endregion

            _requestService = requestService;
            _navigationService = navigationService;
            _logger = logger;

            SignupCommand = new RelayCommand<object>(async (param) => await UserSignup(param), (param) => true);
            NavigateToLoginCommand = new RelayCommand(async () => await UserLogin());
        }


        public string UserNameParam
        { 
            get => userNameParam; 
            
            set
            {
                userNameParam = value;
                OnPropertyChanged(nameof(UserNameParam));
            }
        }

        public string UserSurnameParam
        {
            get => userSurnameParam;

            set
            {
                userSurnameParam = value;
                OnPropertyChanged(nameof(UserSurnameParam));
            }
        }

        public string UserEmailParam
        {
            get => userEmailParam;

            set
            {
                userEmailParam = value;
                OnPropertyChanged(nameof(UserEmailParam));
            }
        }

        public ICommand SignupCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

        public async Task UserSignup(object parameter)
        {
            string password = string.Empty;
            if (parameter is PasswordBox pb)
            {
                password = pb.Password;
            }

            UserDto user = new UserDto()
            {
                UserGuid = Guid.NewGuid(),
                UserName = userNameParam,
                UserSurname = userSurnameParam,
                UserEmail = userEmailParam,
                UserPassword = password,
                UserCreatedOn = DateTime.Now,
            };

            ApiResponse<UserDto> response = await _requestService.AddUser(user);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _logger.LogInformation("SignupViewModel - add user request executed succesfully. User with guid <{userGuid}>", user.UserGuid);
                _navigationService.NavigateTo("Login");
            }
            else
            {
                _logger.LogError("SignupViewModel - add user request not executed. User with guid <{userGuid}>", user.UserGuid);
                _navigationService.NavigateTo("Signup");
            }
        }

        public async Task UserLogin()
        {
            _navigationService.NavigateTo("Login");
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
