using FeatureFlags.Domain.Models;
using FeatureFlags.Domain.Repositories;

namespace FeatureFlags.Infrastructure.Repositories;

public class FlagsRepository : IFlagsRepository
{
    public FlagDomain[] GetAll() =>
    [
        new()
        {
            Id = Guid.NewGuid()
                .ToString(),
            Value = true,
            Label = "greetUser"
        },
        new()
        {
            Id = Guid.NewGuid()
                .ToString(),
            Value = false,
            Label = "aboutSection"
        }
    ];
}