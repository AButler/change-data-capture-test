using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using UserEditor.ViewModels;

namespace UserEditor.Dialogs;

public partial class EditSystemAccessDialog
{
    public static EditSystemAccessDialog CreateDialog(int userId)
    {
        var dialog = App.Current.Services.GetRequiredService<EditSystemAccessDialog>();
        dialog.ViewModel.Initialize(userId);

        return dialog;
    }

    public EditSystemAccessDialog(EditSystemAccessViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }

    public EditSystemAccessViewModel ViewModel
    {
        get => (EditSystemAccessViewModel)DataContext;
        set => DataContext = value;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewModel.LoadCommand.Execute(null);
    }
}
