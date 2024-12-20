using FeatureFlags.Application.Dto;
using FeatureFlags.Domain.Models;

namespace FeatureFlags.Application.Mappers;

public static class FlagMapper
{
    public static FlagDto ToDto(this FlagDomain x) => new(Id: x.Id, Label: x.Label, Value: x.Value);
}