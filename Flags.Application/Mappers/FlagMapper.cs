using Flags.Application.Dto;
using Flags.Domain.Models;

namespace Flags.Application.Mappers;

public static class FlagMapper
{
    public static FlagDto ToDto(this FlagDomain x) =>
        new(Id: x.Id, Label: x.Label, Value: x.Value, Groups: Array.Empty<GroupDto>());
}