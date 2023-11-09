using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using ModernWpf.Controls;
using UserEditor.Dialogs;
using UserEditor.Models;
using UserEditor.Repositories;

namespace UserEditor.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly UserRepository _userRepository;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private IReadOnlyList<UserModel>? _users;

    public ICommand RefreshCommand { get; }
    public ICommand AddUserCommand { get; }
    public ICommand EditUserCommand { get; }
    public ICommand ToggleEnableUserCommand { get; }
    public ICommand EditSystemAccessCommand { get; }
    public ICommand ExitCommand { get; }

    public MainWindowViewModel(UserRepository userRepository)
    {
        _userRepository = userRepository;

        RefreshCommand = new AsyncRelayCommand(Refresh);
        AddUserCommand = new AsyncRelayCommand(AddUser);
        EditUserCommand = new AsyncRelayCommand<UserModel?>(EditUser, u => u != null);
        ToggleEnableUserCommand = new AsyncRelayCommand<UserModel?>(
            ToggleEnableUser,
            u => u != null
        );
        EditSystemAccessCommand = new AsyncRelayCommand<UserModel?>(
            EditSystemAccess,
            u => u != null
        );
        ExitCommand = new RelayCommand(Exit);
    }

    private async Task Refresh()
    {
        IsBusy = true;

        Users = await _userRepository.GetAll();

        IsBusy = false;
    }

    private async Task AddUser()
    {
        var dialog = AddEditUserDialog.CreateAddDialog();
        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            var updatedUser = dialog.ViewModel.CreateUserModel();
            await _userRepository.Upsert(updatedUser);
            RefreshCommand.Execute(null);
        }
    }

    private async Task EditUser(UserModel? user)
    {
        if (user == null)
        {
            return;
        }

        var dialog = AddEditUserDialog.CreateEditDialog(user.Id);
        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            var updatedUser = dialog.ViewModel.CreateUserModel();
            await _userRepository.Upsert(updatedUser);
            RefreshCommand.Execute(null);
        }
    }

    private async Task ToggleEnableUser(UserModel? user)
    {
        if (user == null)
        {
            return;
        }

        if (user.IsDisabled)
        {
            await _userRepository.EnableUser(user.Id);
        }
        else
        {
            await _userRepository.DisableUser(user.Id);
        }

        RefreshCommand.Execute(null);
    }

    private async Task EditSystemAccess(UserModel? user)
    {
        if (user == null)
        {
            return;
        }

        var dialog = EditSystemAccessDialog.CreateDialog(user.Id);
        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            //TODO: update access
        }
    }

    private void Exit()
    {
        Application.Current.Shutdown();
    }
}
