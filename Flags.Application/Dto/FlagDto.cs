namespace Flags.Application.Dto;

public record FlagDto(string Id, string Label, bool Value, IEnumerable<GroupDto> Groups);