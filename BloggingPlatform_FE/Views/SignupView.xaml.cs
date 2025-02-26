using BloggingPlatform_FE.ViewModels;
using System.Windows.Controls;

namespace BloggingPlatform_FE.Views
{
    /// <summary>
    /// Interaction logic for SignupView.xaml
    /// </summary>
    public partial class SignupView : Page
    {
        public SignupView(SignupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
