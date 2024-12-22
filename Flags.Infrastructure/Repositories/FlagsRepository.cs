using Flags.Domain.Errors;
using Flags.Domain.Models;
using Flags.Domain.Repositories;
using Flags.Infrastructure.EFCore;
using Flags.Infrastructure.Mappers;
using Flags.Infrastructure.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Flags.Infrastructure.Repositories;

public sealed class FlagsRepository(FlagsContext flagsContext) : IFlagsRepository
{
    public async Task<FlagDomain[]> GetAll(CancellationToken ct)
    {
        return await flagsContext.Set<FlagDb>().Select(x => x.ToDomain()).ToArrayAsync(ct);
    }

    public async Task<Result<FlagDomain>> PostFlag(string label, CancellationToken ct)
    {
        var flag = new FlagDb { Label = label, Enabled = false };
        await flagsContext.Set<FlagDb>().AddAsync(flag, ct);

        await flagsContext.SaveChangesAsync(ct);

        return Result.Ok(flag.ToDomain());
    }


    public async Task<Result<FlagDomain>> PatchFlag(string id, bool value, CancellationToken ct)
    {
        var idInt = int.Parse(id);

        var found = await flagsContext.Set<FlagDb>().FirstOrDefaultAsync(x => x.Id == idInt, ct);
        if (found == null)
        {
            return Result.Fail<FlagDomain>(new FlagDoesNotExist());
        }

        found.Enabled = value;

        await flagsContext.SaveChangesAsync(ct);

        return Result.Ok(found.ToDomain());
    }
}