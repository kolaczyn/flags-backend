using FluentResults;

namespace Flags.Domain.Errors;

public sealed class FlagDoesNotExist : Error
{
    public FlagDoesNotExist()
        : base("Flag does not exist")
    {
        WithMetadata("ErrorCode", "404_0");
    }
}