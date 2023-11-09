using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using UserEditor.Config;
using UserEditor.Models;

namespace UserEditor.Repositories;

public class SystemAccessRepository
{
    private readonly DatabaseSettings _databaseSettings;

    public SystemAccessRepository(IOptions<DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings.Value;
    }

    public async Task<IReadOnlyList<SystemAccessModel>> GetForUser(int userId)
    {
        await using var connection = new SqlConnection(_databaseSettings.ConnectionString);

        const string query = """
                             SELECT sa.[Id], sa.[SystemId], s.[Name] as [SystemName], sa.[Start], sa.[End]
                             FROM [dbo].[SystemAccess] sa
                             JOIN [dbo].[Systems] s
                               ON sa.[SystemId] = s.[Id] 
                             ORDER BY sa.[Start], sa.[End]
                             """;

        var systemAccess = await connection.QueryAsync<SystemAccessModel>(query);
        return systemAccess.ToList().AsReadOnly();
    }
}
