using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using UserEditor.Config;
using UserEditor.Models;

namespace UserEditor.Repositories;

public class UserRepository
{
    private readonly DatabaseSettings _databaseSettings;

    public UserRepository(IOptions<DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings.Value;
    }

    public async Task<IReadOnlyList<UserModel>> GetAll()
    {
        await using var connection = new SqlConnection(_databaseSettings.ConnectionString);

        const string query = """
                             SELECT u.[Id], u.[UserId], u.[FirstName], u.[LastName], u.[DisplayName], u.[EmailAddress], u.[UserTypeId], ut.[Name] as UserTypeName, u.[IsDisabled]
                             FROM [dbo].[Users] u
                             JOIN [dbo].[UserTypes] ut
                               ON u.[UserTypeId] = ut.[Id]
                             ORDER BY u.UserId
                             """;

        var users = await connection.QueryAsync<UserModel>(query);
        return users.ToList().AsReadOnly();
    }

    public async Task<UserModel> GetById(int userId)
    {
        await using var connection = new SqlConnection(_databaseSettings.ConnectionString);

        const string query = """
                             SELECT u.[Id], u.[UserId], u.[FirstName], u.[LastName], u.[DisplayName], u.[EmailAddress], u.[UserTypeId], ut.[Name] as UserTypeName, u.[IsDisabled]
                             FROM [dbo].[Users] u
                             JOIN [dbo].[UserTypes] ut
                               ON u.[UserTypeId] = ut.[Id]
                             WHERE u.[Id] = @Id
                             """;

        var users = await connection.QueryAsync<UserModel>(query, new { Id = userId });
        return users.Single();
    }

    public async Task Upsert(UserModel user)
    {
        await using var connection = new SqlConnection(_databaseSettings.ConnectionString);

        const string updateQuery = """
                                   UPDATE [dbo].[Users]
                                   SET [UserId] = @UserId,
                                       [FirstName] = @FirstName,
                                       [LastName] = @LastName,
                                       [DisplayName] = @DisplayName,
                                       [EmailAddress] = @EmailAddress,
                                       [UserTypeId] = @UserTypeId,
                                       [IsDisabled] = @IsDisabled
                                   WHERE [Id] = @Id
                                   """;

        const string insertQuery = """
                                   INSERT INTO Users ([UserId], [FirstName], [LastName], [DisplayName], [EmailAddress], [UserTypeId], [IsDisabled])
                                   VALUES (@UserId, @FirstName, @LastName, @DisplayName, @EmailAddress, @UserTypeId, @IsDisabled)
                                   """;

        await connection.QueryAsync<UserModel>(
            user.Id > 0 ? updateQuery : insertQuery,
            new
            {
                user.Id,
                user.UserId,
                user.FirstName,
                user.LastName,
                user.DisplayName,
                user.EmailAddress,
                user.UserTypeId,
                user.IsDisabled
            }
        );
    }

    public async Task EnableUser(int userId)
    {
        await SetUserIsDisabled(userId, false);
    }

    public async Task DisableUser(int userId)
    {
        await SetUserIsDisabled(userId, true);
    }

    private async Task SetUserIsDisabled(int userId, bool isDisabled)
    {
        await using var connection = new SqlConnection(_databaseSettings.ConnectionString);

        const string query =
            "UPDATE [dbo].[Users] SET [IsDisabled] = @IsDisabled WHERE [Id] = @UserId";

        await connection.QueryAsync(query, new { IsDisabled = isDisabled, UserId = userId });
    }
}
