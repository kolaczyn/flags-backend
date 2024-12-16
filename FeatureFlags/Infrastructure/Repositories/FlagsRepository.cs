using FeatureFlags.Domain.Models;
using FeatureFlags.Domain.Repositories;

namespace FeatureFlags.Infrastructure.Repositories;

public class FlagsRepository : IFlagsRepository
{
    private static readonly FlagDomain[] _flags;

    static FlagsRepository()
    {
        _flags =
        [
            new FlagDomain
            {
                Id = Guid.NewGuid()
                    .ToString(),
                Value = true,
                Label = "greetUser"
            },
            new FlagDomain
            {
                Id = Guid.NewGuid()
                    .ToString(),
                Value = false,
                Label = "aboutSection"
            }
        ];
    }

    public FlagDomain[] GetAll() => _flags;
}