using Flags.Domain.Models;
using FluentResults;

namespace Flags.Domain.Repositories;

public interface IFlagsRepository
{
    public Task<FlagDomain[]> GetAll(CancellationToken ct);
    public Task<Result<FlagDomain>> PatchFlag(string id, bool value, CancellationToken ct);
}