using FeatureFlags.Application.Dto;
using FeatureFlags.Application.Mappers;
using FeatureFlags.Domain.Errors;
using FeatureFlags.Domain.Repositories;

namespace FeatureFlags.Application.UseCases;

public sealed class PatchFlagUseCase(IFlagsRepository repository)
{
    public (FlagDto?, IAppError?) Execute(string id, PatchFlagCmd cmd, CancellationToken ct)
    {
        var (result, err) = repository.PatchFlag(id, cmd.Value, ct);

        var domain = result?.ToDto();
        return (domain, err);
    }
}