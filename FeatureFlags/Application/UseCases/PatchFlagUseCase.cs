using FeatureFlags.Application.Dto;
using FeatureFlags.Application.Mappers;
using FeatureFlags.Domain.Errors;
using FeatureFlags.Domain.Repositories;

namespace FeatureFlags.Application.UseCases;

public class PatchFlagUseCase(IFlagsRepository repository)
{
    public (FlagDto?, IAppError?) Execute(string id, PatchFlagCommand command, CancellationToken ct)
    {
        var (result, err) = repository.PatchFlag(id, command.Value, ct);

        var domain = result?.ToDto();
        return (domain, err);
    }
}