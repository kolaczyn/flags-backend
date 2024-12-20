namespace Flags.Domain.Errors;

public sealed class FlagDoesNotExist : IAppError
{
    public int Id { get; init; } = 404_0;
    public string Message { get; init; } = "Flag does not exist";
}