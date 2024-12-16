using FeatureFlags.Application.Dto;
using FeatureFlags.Application.Mappers;
using FeatureFlags.Domain.Errors;
using FeatureFlags.Domain.Repositories;

namespace FeatureFlags.Application.UseCases;

public class PatchFlagUseCase(IFlagsRepository repository)
{
    public (FlagDto?, IAppError?) Execute(string id, PatchFlagDto dto)
    {
        var (result, err) = repository.PatchFlag(id, dto.Value);

        var domain = result?.ToDto();
        return (domain, err);
    }
}