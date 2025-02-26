using BloggingPlatform_FE.Interfaces;
using BloggingPlatform_FE.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BloggingPlatform_FE.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            // todo fondamentale, risolvendo le dipendenze con di, invece di passare qua l'oggetto nuovo,
            // qli passo un oggetto passato dal costruttore, che verrà automaticamente risolto
            // se sottoscritto in app.xaml.cs
            // questo perchè il view model dipende da altri servizi, e chiamare qua il provider sarebbe ridondante
            DataContext = viewModel;
        }

        // at the startup, the login signup dialog will be opened, the returned value will select the correct view to show
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var dialog = new LoginSignupDialogView(new LoginSignupDialogViewModel());

            // getting the result of the login signup dialog
            bool? result = dialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                // getting the navigation service from the di container
                var navigationService = App.ServiceProvider.GetRequiredService<INavigationService>();

                if (dialog.ChosenOption == "Login")
                {
                    navigationService.NavigateTo("Login");
                }
                else if (dialog.ChosenOption == "Signup")
                {
                    navigationService.NavigateTo("Signup");
                }
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}