using Flags.Application.Dto;
using Flags.Application.Mappers;
using Flags.Domain.Errors;
using Flags.Domain.Repositories;

namespace Flags.Application.UseCases;

public sealed class PatchFlagUseCase(IFlagsRepository repository)
{
    public (FlagDto?, IAppError?) Execute(string id, PatchFlagCmd cmd, CancellationToken ct)
    {
        var (result, err) = repository.PatchFlag(id, cmd.Value, ct);

        var domain = result?.ToDto();
        return (domain, err);
    }
}