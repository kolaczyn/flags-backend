using FeatureFlags.Domain.Models;

namespace FeatureFlags.Application.Mappers;

public static class FlagMapper
{
    public static FlagsDto ToDto(this FlagDomain domain) => new()
    {
        Id = domain.Id,
        Label = domain.Label,
        Value = domain.Value,
    };
}