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

    public async Task<IReadOnlyList<UserModel>> GetUsers()
    {
        await using var connection = new SqlConnection(_databaseSettings.ConnectionString);

        var query =
            "SELECT u.[Id], u.[UserId], u.[FirstName], u.[LastName], u.[DisplayName], u.[EmailAddress] FROM [dbo].[Users] u ORDER BY u.UserId";

        var users = await connection.QueryAsync<UserModel>(query);
        return users.ToList().AsReadOnly();
    }
}
