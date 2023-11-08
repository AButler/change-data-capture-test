namespace UserEditor.Models;

public record UserModel(
    string Id,
    string FirstName,
    string LastName,
    string DisplayName,
    string EmailAddress
);
