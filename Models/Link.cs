using Dapper.Contrib.Extensions;

namespace Krtshk.Models;

[Table("Links")]
public class Link
{
    [ExplicitKey]
    public required string Key { get; set; }

    public required string Url { get; set; }
}