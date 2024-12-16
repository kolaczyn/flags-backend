using FeatureFlags.Application.Dto;
using FeatureFlags.Application.Mappers;
using FeatureFlags.Domain.Repositories;

namespace FeatureFlags.Application.UseCases;

public class GetAllFlagsUseCase(IFlagsRepository repository)
{
    public IEnumerable<FlagDto> Execute()
    {
        var result = repository.GetAll();
        return result.Select(x => x.ToDto());
    }
}