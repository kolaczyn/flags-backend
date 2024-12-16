using FeatureFlags.Domain.Models;

namespace FeatureFlags.Domain.Repositories;

public interface IFlagsRepository
{
    public FlagDomain[] GetAll();
}