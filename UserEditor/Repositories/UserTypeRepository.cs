using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using UserEditor.Config;
using UserEditor.Models;

namespace UserEditor.Repositories;

public class UserTypeRepository
{
    private readonly DatabaseSettings _databaseSettings;

    public UserTypeRepository(IOptions<DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings.Value;
    }
    
    public async Task<IReadOnlyList<UserTypeModel>> GetAll()
    {
        await using var connection = new SqlConnection(_databaseSettings.ConnectionString);

        const string query = """
                             SELECT ut.[Id], ut.[Name]
                             FROM [dbo].[UserTypes] ut
                             ORDER BY ut.Id
                             """;

        var userTypes = await connection.QueryAsync<UserTypeModel>(query);
        return userTypes.ToList().AsReadOnly();
    }
}