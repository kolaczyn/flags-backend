using Flags.Domain.Models;
using FluentResults;

namespace Flags.Domain.Repositories;

public interface IFlagsRepository
{
    // Now that it uses an actual db, this should return Result
    public Task<FlagDomain[]> GetAll(CancellationToken ct);
    public Task<Result<FlagDomain>> PostFlag(string label, CancellationToken ct);
    public Task<Result<FlagDomain>> PatchFlag(string id, bool value, CancellationToken ct);
}