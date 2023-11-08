using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

    public ICommand LoadCommand { get; }
    public ICommand AddUserCommand { get; }
    public ICommand EditUserCommand { get; }
    public ICommand ExitCommand { get; }

    public MainWindowViewModel(UserRepository userRepository)
    {
        _userRepository = userRepository;

        LoadCommand = new AsyncRelayCommand(Load);
        AddUserCommand = new RelayCommand(AddUser);
        EditUserCommand = new RelayCommand<UserModel?>(EditUser, u => u != null);
        ExitCommand = new RelayCommand(Exit);
    }

    private async Task Load()
    {
        IsBusy = true;

        Users = await _userRepository.GetUsers();

        IsBusy = false;
    }

    private void AddUser()
    {
        MessageBox.Show("Add user");
    }

    private void EditUser(UserModel? user)
    {
        MessageBox.Show($"Edit user {user.DisplayName}");
    }

    private void Exit()
    {
        Application.Current.Shutdown();
    }
}
