namespace UserEditor.Models;

public record UserModel(
    int Id,
    string UserId,
    string FirstName,
    string LastName,
    string DisplayName,
    string EmailAddress,
    int UserTypeId,
    string UserTypeName,
    bool IsDisabled
);
