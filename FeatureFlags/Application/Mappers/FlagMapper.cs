using FeatureFlags.Application.Dto;
using FeatureFlags.Domain.Models;

namespace FeatureFlags.Application.Mappers;

public static class FlagMapper
{
    public static FlagDto ToDto(this FlagDomain domain) => new()
    {
        Id = domain.Id,
        Label = domain.Label,
        Value = domain.Value,
    };
}