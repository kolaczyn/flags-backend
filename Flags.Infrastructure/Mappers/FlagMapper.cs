using Flags.Domain.Models;
using Flags.Infrastructure.Models;

namespace Flags.Infrastructure.Mappers;

public static class FlagMapper
{
    public static FlagDomain ToDomain(this FlagDb x) => new(Id: x.Id.ToString(), Label: x.Label, Value: x.Enabled);
}