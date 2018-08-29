using System.Windows;
using WordsLearning.ViewModels;

namespace WordsLearning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MainWindowViewModel _mainWindowViewModel;

        MainWindowViewModel MainWindowViewModel { get => _mainWindowViewModel ?? (_mainWindowViewModel = DataContext as MainWindowViewModel); }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                MainWindowViewModel.ShowSolution();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindowViewModel.WindowClosing();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.WindowLoaded();
        }

        private void NewWordButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.NextWordClick();
        }

        private void ShowCurrentListButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.ShowCurrentList();
        }
    }
}
