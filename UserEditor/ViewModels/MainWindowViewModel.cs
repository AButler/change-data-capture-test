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
    public ICommand ExitCommand { get; }

    public MainWindowViewModel(UserRepository userRepository)
    {
        _userRepository = userRepository;

        LoadCommand = new AsyncRelayCommand(Load);
        ExitCommand = new RelayCommand(Exit);
    }

    private async Task Load()
    {
        IsBusy = true;

        Users = await _userRepository.GetUsers();

        IsBusy = false;
    }

    private void Exit()
    {
        Application.Current.Shutdown();
    }
}
