using BloggingPlatform_FE.ViewModels;
using System.Windows;

namespace BloggingPlatform_FE.Views
{
    /// <summary>
    /// Interaction logic for LoginSignupDialog.xaml
    /// </summary>
    public partial class LoginSignupDialogView : Window
    {
        private string _chosenOption;

        public string ChosenOption
        {
            get => _chosenOption;
            set => _chosenOption = value;
        }

        public LoginSignupDialogView(LoginSignupDialogViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e) 
        {
            ChosenOption = "Login";
            DialogResult = true;
            Close();
        }

        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            ChosenOption = "Signup";
            DialogResult = true;
            Close();
        }

    }
}

