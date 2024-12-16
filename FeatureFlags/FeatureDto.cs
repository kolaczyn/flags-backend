namespace FeatureFlags;

public class FeatureDto
{
    public required string Id { get; init; }
    public required string Label { get; init; }

    public required bool Value { get; init; }
    // public required GroupDto[] Groups { get; init; }
}