using System.Reflection.Emit;
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
        new(Id: "1", Label: "greetUser", Value: true),
        new(Id: "2", Label: "aboutSection", Value: false)
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

        _flags = _flags.Select(x => x.Id == id ? new FlagDomain(Id: id, Label: x.Label, Value: value) : x).ToArray();

        var foundAfterChange = _flags.FirstOrDefault(x => x.Id == id)!;


        return (foundAfterChange, null);
    }

// TODO refactor this later
    private static NpgsqlConnection GetConnection()
    {
        const string connectionString = "Host=localhost;Username=postgres;Password=12345678;Database=postgres";
        // NpgsqlDataSource.create lepiej reużywa połączenia
        // Npgsql.DepndencyInjection


        return new NpgsqlConnection(connectionString);
    }

    private async Task<string> TestConnection()
    {
        await using var connection = GetConnection();
        var result = await connection.QueryFirstAsync<string>("SELECT 'Hello World'");
        return result ?? "Nothing";
    }
}