using BloggingPlatform_FE.ViewModels;
using System.Windows.Controls;

namespace BloggingPlatform_FE.Views
{
    /// <summary>
    /// Interaction logic for WritePostView.xaml
    /// </summary>
    public partial class WritePostView : Page
    {
        public WritePostView(WritePostViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
