using Flags.Domain.Errors;
using Flags.Domain.Models;

namespace Flags.Domain.Repositories;

public interface IFlagsRepository
{
    public Task<FlagDomain[]> GetAll(CancellationToken ct);
    public (FlagDomain?, IAppError?) PatchFlag(string id, bool value, CancellationToken ct);
}