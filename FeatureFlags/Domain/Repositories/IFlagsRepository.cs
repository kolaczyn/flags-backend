using FeatureFlags.Domain.Errors;
using FeatureFlags.Domain.Models;

namespace FeatureFlags.Domain.Repositories;

public interface IFlagsRepository
{
    public Task<FlagDomain[]> GetAll();
    public (FlagDomain?, IAppError?) PatchFlag(string id, bool value);
}