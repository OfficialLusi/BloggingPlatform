using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.Services;
using BloggingPlatform_FE.ViewModels;
using BloggingPlatform_FE.Views;
using LusiUtilsLibrary.Backend.APIs_REST;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Windows;

namespace BloggingPlatform_FE
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Logging subscription
            AddLogging(services);

            // Core Services subscription
            AddCoreServices(services);

            // ViewModels subscription
            AddViewModels(services);

            // Views subscription
            AddViews(services);
        }

        private static void AddLogging(IServiceCollection services)
        {
            services.AddLogging(configure =>
            {
                configure.AddConsole();
                configure.SetMinimumLevel(LogLevel.Information);
            });
        }

        private static void AddCoreServices(IServiceCollection services)
        {
            // services subscription
            // rest service from library
            string workingDirectory = Environment.CurrentDirectory;
            string requestFileName = "\\communicationsettings.json";
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName + requestFileName;

            services.AddSingleton<IREST_RequestService, REST_RequestService>(provider =>
            {
                ILogger<REST_RequestService> logger = provider.GetRequiredService<ILogger<REST_RequestService>>();
                return new REST_RequestService(logger, projectDirectory);
            });

            // rest service for frontend
            services.AddSingleton<IRequestService_FE, RequestService_FE>(provider =>
            {
                ILogger<IRequestService_FE> logger = provider.GetRequiredService<ILogger<IRequestService_FE>>();
                IREST_RequestService requestService = provider.GetRequiredService<IREST_RequestService>();
                return new RequestService_FE(logger, requestService);
            });

            // page navigation service
            services.AddSingleton<INavigationService, NavigationService>(provider =>
            {
                return new NavigationService(provider);
            });

            // memory service
            services.AddSingleton<IMemoryService, MemoryService>();

            // compare service
            services.AddTransient<CompareService>();
        }

        private static void AddViewModels(IServiceCollection services)
        {
            services.AddTransient<MainViewModel>();
            services.AddTransient<LoginSignupDialogViewModel>();

            services.AddTransient(provider =>
            {
                IRequestService_FE requestService = provider.GetRequiredService<IRequestService_FE>();
                INavigationService navigationService = provider.GetRequiredService<INavigationService>();
                IMemoryService memoryService = provider.GetRequiredService<IMemoryService>();
                return new LoginViewModel(requestService, navigationService, memoryService);
            });

            services.AddTransient(provider =>
            {
                IRequestService_FE requestService = provider.GetRequiredService<IRequestService_FE>();
                INavigationService navigationService = provider.GetRequiredService<INavigationService>();
                ILogger<SignupViewModel> logger = provider.GetRequiredService<ILogger<SignupViewModel>>();
                return new SignupViewModel(requestService, navigationService, logger);
            });

            services.AddTransient(provider =>
            {
                IRequestService_FE requestService = provider.GetRequiredService<IRequestService_FE>();
                INavigationService navigationService = provider.GetRequiredService<INavigationService>();
                CompareService compareService = provider.GetRequiredService<CompareService>();
                ILogger<HomeViewModel> logger = provider.GetRequiredService<ILogger<HomeViewModel>>();
                return new HomeViewModel(requestService, navigationService, compareService, logger);
            });

            services.AddTransient(provider =>
            {
                IRequestService_FE requestService = provider.GetRequiredService<IRequestService_FE>();
                INavigationService navigationService = provider.GetRequiredService<INavigationService>();
                IMemoryService memoryService = provider.GetRequiredService<IMemoryService>();
                CompareService compareService = provider.GetRequiredService<CompareService>();
                ILogger<PersonalPostViewModel> logger = provider.GetRequiredService<ILogger<PersonalPostViewModel>>();
                return new PersonalPostViewModel(requestService, navigationService, memoryService, compareService, logger);
            });

            services.AddTransient(provider =>
            {
                IRequestService_FE requestService = provider.GetRequiredService<IRequestService_FE>();
                INavigationService navigationService = provider.GetRequiredService<INavigationService>();
                IMemoryService memoryService = provider.GetRequiredService<IMemoryService>();
                ILogger<WritePostViewModel> logger = provider.GetRequiredService<ILogger<WritePostViewModel>>();
                return new WritePostViewModel(requestService, navigationService, memoryService, logger);
            });

            services.AddTransient(provider =>
            {
                IRequestService_FE requestService = provider.GetRequiredService<IRequestService_FE>();
                INavigationService navigationService = provider.GetRequiredService<INavigationService>();
                IMemoryService memoryService = provider.GetRequiredService<IMemoryService>();
                ILogger<EditPostViewModel> logger = provider.GetRequiredService<ILogger<EditPostViewModel>>();
                return new EditPostViewModel(requestService, navigationService, memoryService, logger);
            });

            services.AddTransient<DeleteConfirmationDialogViewModel>();
        }

        private static void AddViews(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();

            services.AddTransient<LoginView>();
            services.AddTransient<SignupView>();
            services.AddTransient<HomeView>();
            services.AddTransient<LoginSignupDialogView>();
            services.AddTransient<PersonalPostView>();
            services.AddTransient<WritePostView>();
            services.AddTransient<DeleteConfirmationDialogView>();
            services.AddTransient<EditPostView>();
        }

    }

}
