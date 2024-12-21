using Dapper;
using Flags.Domain.Errors;
using Flags.Domain.Models;
using Flags.Domain.Repositories;
using FluentResults;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Flags.Infrastructure.Repositories;

public sealed class FlagsRepository(IConfiguration configuration) : IFlagsRepository
{
    private readonly IConfiguration _configuration = configuration;
    // private NpgsqlConnection _connection;

    private FlagDomain[] _flags =
    [
        new(Id: "1", Label: "greetUser", Value: true),
        new(Id: "2", Label: "aboutSection", Value: false)
    ];

    public async Task<FlagDomain[]> GetAll(CancellationToken ct)
    {
        var rows = await TestConnection(ct);
        Console.WriteLine($"Got: {rows}");
        return _flags;
    }

    public Result<FlagDomain> PatchFlag(string id, bool value, CancellationToken ct)
    {
        var found = _flags.FirstOrDefault(x => x.Id == id);
        if (found == null)
        {
            return Result.Fail<FlagDomain>(new FlagDoesNotExist());
        }

        _flags = _flags.Select(x => x.Id == id ? new FlagDomain(Id: id, Label: x.Label, Value: value) : x).ToArray();

        var foundAfterChange = _flags.FirstOrDefault(x => x.Id == id)!;

        return Result.Ok<FlagDomain>(foundAfterChange);
    }

    private string ConnectionString() =>
        // TODO I should throw error if it's null
        _configuration.GetConnectionString("PostgresConnection")!;

    // TODO refactor this later
    private NpgsqlConnection GetConnection()
    {
        // NpgsqlDataSource.create lepiej reużywa połączenia
        // Npgsql.DependencyInjection
// 
        // return new NpgsqlConnection(ConnectionString());
        return new NpgsqlConnection("Host=127.0.0.1;Port=32794;Database=postgres;Username=postgres;Password=postgres");
    }

    private async Task<string> TestConnection(CancellationToken ct)
    {
        await using var connection = GetConnection();
        var result = await connection.QueryFirstAsync<string>("SELECT 'Hello World'", ct);
        return result ?? "Nothing";
    }
}