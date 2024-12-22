using Flags.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Flags.Infrastructure.EFCore;

public sealed class FlagsContext(DbContextOptions<FlagsContext> options) : DbContext(options)
{
    public DbSet<FlagDb> Features { get; set; }
}