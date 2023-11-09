using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using UserEditor.ViewModels;

namespace UserEditor.Dialogs;

public partial class AddEditUserDialog
{
    public static AddEditUserDialog CreateAddDialog()
    {
        return App.Current.Services.GetRequiredService<AddEditUserDialog>();
    }

    public static AddEditUserDialog CreateEditDialog(int userId)
    {
        var dialog = App.Current.Services.GetRequiredService<AddEditUserDialog>();
        dialog.ViewModel.Initialize(userId);
        return dialog;
    }

    public AddEditUserDialog(AddEditUserViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }

    public AddEditUserViewModel ViewModel
    {
        get => (AddEditUserViewModel)DataContext;
        private set => DataContext = value;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewModel.LoadCommand.Execute(null);
    }
}
