namespace Flags.Infrastructure.Models;

public sealed class FlagDb
{
    public int Id { get; set; }
    public string Label { get; set; }
    public bool Enabled { get; set; }
}