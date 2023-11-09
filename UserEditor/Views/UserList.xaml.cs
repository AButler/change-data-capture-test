using System.Windows.Controls;
using System.Windows.Input;
using UserEditor.ViewModels;

namespace UserEditor.Views;

public partial class UserList
{
    public UserList()
    {
        InitializeComponent();
    }

    private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var listView = (ListView)sender;

        if (listView.SelectedItem == null)
        {
            return;
        }

        var viewModel = (MainWindowViewModel)DataContext;
        viewModel.EditUserCommand.Execute(listView.SelectedItem);
    }
}
