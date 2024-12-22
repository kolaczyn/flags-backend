using FluentResults;

namespace Flags.Domain.Errors;

// TODO will have to use fluent validation instead
public sealed class FlagLabelTooShort : Error
{
    public FlagLabelTooShort()
        : base("Flag label is too short")
    {
        WithMetadata("ErrorCode", "400_0");
    }
}