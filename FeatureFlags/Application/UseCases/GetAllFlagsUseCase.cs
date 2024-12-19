using FeatureFlags.Application.Dto;
using FeatureFlags.Application.Mappers;
using FeatureFlags.Domain.Repositories;

namespace FeatureFlags.Application.UseCases;

public class GetAllFlagsUseCase(IFlagsRepository repository)
{
    public async Task<IEnumerable<FlagDto>> Execute(CancellationToken ct)
    {
        var result = await repository.GetAll(ct);
        return result.Select(x => x.ToDto());
    }
}