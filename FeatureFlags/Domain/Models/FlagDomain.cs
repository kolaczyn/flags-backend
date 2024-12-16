namespace FeatureFlags.Domain.Models;

public class FlagDomain
{
    public required string Id { get; init; }
    public required string Label { get; init; }

    public required bool Value { get; init; }
}