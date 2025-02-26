using BloggingPlatform_FE.ViewModels;
using System.Windows;

namespace BloggingPlatform_FE.Views
{
    /// <summary>
    /// Interaction logic for DeleteConfirmationDialogView.xaml
    /// </summary>
    public partial class DeleteConfirmationDialogView : Window
    {
        public DeleteConfirmationDialogView(DeleteConfirmationDialogViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }


        private string _chosenOption;

        public string ChosenOption
        {
            get => _chosenOption;
            set => _chosenOption = value;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            ChosenOption = "Yes";
            DialogResult = true;
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            ChosenOption = "No";
            DialogResult = true;
            Close();
        }
    }
}
