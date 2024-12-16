using FeatureFlags.Domain.Errors;
using FeatureFlags.Domain.Models;
using FeatureFlags.Domain.Repositories;

namespace FeatureFlags.Infrastructure.Repositories;

public class FlagsRepository : IFlagsRepository
{
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

    public FlagDomain[] GetAll() => _flags;

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
}