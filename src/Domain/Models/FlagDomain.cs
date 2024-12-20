namespace FeatureFlags.Domain.Models;

public record FlagDomain(string Id, string Label, bool Value);