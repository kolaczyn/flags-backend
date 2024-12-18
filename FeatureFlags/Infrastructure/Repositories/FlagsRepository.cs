using Dapper;
using FeatureFlags.Domain.Errors;
using FeatureFlags.Domain.Models;
using FeatureFlags.Domain.Repositories;
using Npgsql;

namespace FeatureFlags.Infrastructure.Repositories;

public class FlagsRepository : IFlagsRepository
{
    private NpgsqlConnection _connection;

    private FlagDomain[] _flags =
    [
        new()
        {
            Id = "1",
            Value = true,
            Label = "greetUser"
        },
        new()
        {
            Id = "2",
            Value = false,
            Label = "aboutSection"
        }
    ];

    public async Task<FlagDomain[]> GetAll()
    {
        var rows = await TestConnection();
        Console.WriteLine($"Got: {rows}");
        return _flags;
    }

    public (FlagDomain?, IAppError?) PatchFlag(string id, bool value)
    {
        var found = _flags.FirstOrDefault(x => x.Id == id);
        if (found == null)
        {
            return (null, new FlagDoesNotExist());
        }

        foreach (var flag in _flags)
        {
            if (flag.Id == id)
            {
                flag.Value = value;
            }
        }

        return (found, null);
    }

    // TODO refactor this later
    private static NpgsqlConnection GetConnection()
    {
        const string connectionString = "Host=localhost;Username=postgres;Password=12345678;Database=postgres";
        return new NpgsqlConnection(connectionString);
    }

    private async Task<string> TestConnection()
    {
        await using var connection = GetConnection();
        var result = await connection.QueryFirstAsync<string>("SELECT 'Hello World'");
        return result ?? "Nothing";
    }
}