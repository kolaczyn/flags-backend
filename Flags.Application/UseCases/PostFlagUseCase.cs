using Flags.Application.Dto;
using Flags.Application.Mappers;
using Flags.Domain.Errors;
using Flags.Domain.Repositories;
using FluentResults;

namespace Flags.Application.UseCases;

public sealed class PostFlagUseCase(IFlagsRepository flagsRepository)
{
    public async Task<Result<FlagDto>> Execute(PostFlagCmd cmd, CancellationToken ct)
    {
        if (cmd.Label.Length < 4)
        {
            return Result.Fail<FlagDto>(new FlagLabelTooShort());
        }

        var result = await flagsRepository.PostFlag(cmd.Label, ct);

        return result.Map(x => x.ToDto());
    }
}