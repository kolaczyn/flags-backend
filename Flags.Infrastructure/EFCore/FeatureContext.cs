using Flags.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Flags.Infrastructure.EFCore;

public sealed class FeatureContext : DbContext
{
    public DbSet<FeatureDb> Features { get; set; }

    public FeatureContext(DbContextOptions<FeatureContext> options) : base(options)
    {
    }
}