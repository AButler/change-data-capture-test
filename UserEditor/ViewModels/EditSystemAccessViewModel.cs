using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UserEditor.Models;
using UserEditor.Repositories;

namespace UserEditor.ViewModels;

public partial class EditSystemAccessViewModel : ObservableObject
{
    private readonly SystemsRepository _systemsRepository;
    private readonly SystemAccessRepository _systemAccessRepository;

    private int _userId = -1;

    [ObservableProperty]
    private IReadOnlyList<SystemModel> _systems = new List<SystemModel>();

    [ObservableProperty]
    private IReadOnlyList<SystemAccessModel> _systemAccess = new List<SystemAccessModel>();

    public ICommand LoadCommand { get; }

    public EditSystemAccessViewModel(
        SystemsRepository systemsRepository,
        SystemAccessRepository systemAccessRepository
    )
    {
        _systemsRepository = systemsRepository;
        _systemAccessRepository = systemAccessRepository;

        LoadCommand = new AsyncRelayCommand(Load);
    }

    public void Initialize(int userId)
    {
        _userId = userId;
    }

    private async Task Load()
    {
        if (_userId < 0)
        {
            throw new InvalidOperationException("User not initialized");
        }

        Systems = await _systemsRepository.GetAll();
        SystemAccess = await _systemAccessRepository.GetForUser(_userId);
    }
}
