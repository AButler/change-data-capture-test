using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using UserEditor.Config;
using UserEditor.Models;

namespace UserEditor.Repositories;

public class SystemsRepository
{
    private readonly DatabaseSettings _databaseSettings;

    public SystemsRepository(IOptions<DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings.Value;
    }

    public async Task<IReadOnlyList<SystemModel>> GetAll()
    {
        await using var connection = new SqlConnection(_databaseSettings.ConnectionString);

        const string query = """
                             SELECT s.[Id], s.[Name]
                             FROM [dbo].[Systems] s
                             ORDER BY s.[Name]
                             """;

        var systems = await connection.QueryAsync<SystemModel>(query);
        return systems.ToList().AsReadOnly();
    }
}
