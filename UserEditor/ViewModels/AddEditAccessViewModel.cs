using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UserEditor.Models;
using UserEditor.Repositories;

namespace UserEditor.ViewModels;

public partial class AddEditAccessViewModel : ObservableObject
{
    private readonly SystemsRepository _systemsRepository;
    private int? _id;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private bool _isBusy;

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _okButtonText;

    [ObservableProperty]
    private int? _systemId;

    [ObservableProperty]
    private IReadOnlyList<SystemModel> _systems = new List<SystemModel>();

    public bool IsValid
    {
        get => SystemId.HasValue;
    }

    public ICommand LoadCommand { get; }

    public AddEditAccessViewModel(SystemsRepository systemsRepository)
    {
        _title = "Add Access Control Rule";
        _okButtonText = "Add Access Control Rule";

        _systemsRepository = systemsRepository;

        LoadCommand = new AsyncRelayCommand(Load);
    }

    public void Initialize(int systemAccessId)
    {
        Title = "Edit Access Control Rule";
        OkButtonText = "Update Access Control Rule";
        _id = systemAccessId;
    }

    private async Task Load()
    {
        IsBusy = true;

        Systems = await _systemsRepository.GetAll();

        if (_id == null)
        {
            IsBusy = false;
            return;
        }

        IsBusy = false;
    }
}
