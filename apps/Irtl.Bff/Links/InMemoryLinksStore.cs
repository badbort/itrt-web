using System.Runtime.CompilerServices;
using Irtl.Bff.Links.Model;

namespace Irtl.Bff.Links;

public class InMemoryLinksStore : ILinksStore
{
    private readonly List<UrlLink> _links = new(); 
    
    public async Task AddLink(UrlLink link, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        _links.Add(link);
    }

    public async IAsyncEnumerable<UrlLink> GetLinks([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        foreach (var link in _links)
        {
            yield return link;
        }   
    }
}

public interface ILinksStore
{
    Task AddLink(UrlLink link, CancellationToken cancellationToken);
    IAsyncEnumerable<UrlLink> GetLinks(CancellationToken cancellationToken);
}