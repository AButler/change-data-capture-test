using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UserEditor.Models;
using UserEditor.Repositories;

namespace UserEditor.ViewModels;

public partial class AddEditUserViewModel : ObservableObject
{
    private readonly UserRepository _userRepository;
    private readonly UserTypeRepository _userTypeRepository;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private bool _isBusy;

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _okButtonText;

    private int? _id;

    [ObservableProperty]
    private IReadOnlyList<UserTypeModel> _userTypes = new List<UserTypeModel>();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string _userId = "";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string _firstName = "";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string _lastName = "";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string _displayName = "";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private string _emailAddress = "";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsValid))]
    private int? _userTypeId;

    [ObservableProperty]
    private bool _isDisabled;

    public bool IsValid
    {
        get =>
            !IsBusy
            && UserId.Length > 0
            && FirstName.Length > 0
            && LastName.Length > 0
            && DisplayName.Length > 0
            && EmailAddress.Length > 0
            && UserTypeId.HasValue;
    }

    public ICommand LoadCommand { get; }

    public AddEditUserViewModel(
        UserRepository userRepository,
        UserTypeRepository userTypeRepository
    )
    {
        _title = "Add User";
        _okButtonText = "Create User";

        _userRepository = userRepository;
        _userTypeRepository = userTypeRepository;

        LoadCommand = new AsyncRelayCommand(Load);
    }

    public void Initialize(int userId)
    {
        Title = "Edit User";
        OkButtonText = "Update User";
        _id = userId;
    }

    private async Task Load()
    {
        IsBusy = true;

        UserTypes = await _userTypeRepository.GetAll();

        if (_id == null)
        {
            IsBusy = false;
            return;
        }

        var user = await _userRepository.GetById(_id.Value);

        UserId = user.UserId;
        FirstName = user.FirstName;
        LastName = user.LastName;
        DisplayName = user.DisplayName;
        EmailAddress = user.EmailAddress;
        UserTypeId = user.UserTypeId;
        IsDisabled = user.IsDisabled;

        IsBusy = false;
    }

    public UserModel CreateUserModel()
    {
        if (!IsValid)
        {
            throw new Exception("Invalid user model");
        }

        return new UserModel(
            _id.GetValueOrDefault(0),
            UserId,
            FirstName,
            LastName,
            DisplayName,
            EmailAddress,
            UserTypeId!.Value,
            UserTypes.Single(ut => ut.Id == UserTypeId.Value).Name,
            IsDisabled
        );
    }
}
