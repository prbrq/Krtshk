using Dapper.Contrib.Extensions;

using Krtshk.Models;

namespace Krtshk.Repositories;

public interface ILinkRepository
{
    Task AddLinkAsync(Link link);

    Task<Link> GetLinkAsync(Guid uuid);
}

public class LinkRepository(IDatabaseContext context) : ILinkRepository
{
    public Task AddLinkAsync(Link link)
    {
        using var connection = context.CreateConnection();

        return connection.InsertAsync(link);
    }

    public Task<Link> GetLinkAsync(Guid uuid)
    {
        using var connection = context.CreateConnection();

        return connection.GetAsync<Link>(uuid);
    }
}