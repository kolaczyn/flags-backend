using FeatureFlags.Application.Mappers;
using FeatureFlags.Domain.Repositories;

namespace FeatureFlags.Application.UseCases;

public class GetAllFlagsUseCase(IFlagsRepository repository)
{
    public IEnumerable<FlagsDto> Execute()
    {
        var result = repository.GetAll();
        return result.Select(x => x.ToDto());
    }
}