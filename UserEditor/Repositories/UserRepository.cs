using System.Collections.Generic;
using System.Threading.Tasks;
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
        await Task.Delay(100);
        return new List<UserModel>
        {
            new(
                "bob.bobertson",
                "Bobert",
                "Bobertson",
                "Bob Bobertston",
                "bob.bobertson@sample.com"
            ),
            new(
                "bobetta.bobertson",
                "Bobetta",
                "Bobertson",
                "Bobetta Bobertston",
                "bobetta.bobertson@sample.com"
            ),
        }.AsReadOnly();
    }
}
