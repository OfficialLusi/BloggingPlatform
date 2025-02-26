using BloggingPlatform_FE.ViewModels;
using System.Windows.Controls;

namespace BloggingPlatform_FE.Views
{
    /// <summary>
    /// Interaction logic for PersonalPostView.xaml
    /// </summary>
    public partial class PersonalPostView : Page
    {
        public PersonalPostView(PersonalPostViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;    
        }
    }
}
