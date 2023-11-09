using System.Threading.Tasks;
using System.Windows;
using UserEditor.ViewModels;

namespace UserEditor;

public partial class MainWindow
{
    public MainWindowViewModel ViewModel
    {
        get => (MainWindowViewModel)DataContext;
        private set => DataContext = value;
    }

    public MainWindow(MainWindowViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewModel.RefreshCommand.Execute(null);
    }
}
