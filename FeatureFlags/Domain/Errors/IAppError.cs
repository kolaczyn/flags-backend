namespace FeatureFlags.Domain.Errors;

public class IAppError
{
    public int Id { get; init; }
    public string Message { get; init; }
}