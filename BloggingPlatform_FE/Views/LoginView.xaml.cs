using BloggingPlatform_FE.ViewModels;
using System.Windows.Controls;

namespace BloggingPlatform_FE.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
