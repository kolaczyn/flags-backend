using Flags.Application.Dto;
using Flags.Application.Mappers;
using Flags.Domain.Repositories;

namespace Flags.Application.UseCases;

public sealed class GetAllFlagsUseCase(IFlagsRepository repository)
{
    public async Task<IEnumerable<FlagDto>> Execute(CancellationToken ct)
    {
        var result = await repository.GetAll(ct);
        return result.Select(x => x.ToDto());
    }
}