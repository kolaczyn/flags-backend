using Flags.Application.Dto;
using Flags.Application.Mappers;
using Flags.Domain.Errors;
using Flags.Domain.Repositories;
using FluentResults;

namespace Flags.Application.UseCases;

public sealed class PatchFlagUseCase(IFlagsRepository repository)
{
    public Result<FlagDto> Execute(string id, PatchFlagCmd cmd, CancellationToken ct)
    {
        var result = repository.PatchFlag(id, cmd.Value, ct);

        return result.Map(x => x.ToDto());
    }
}