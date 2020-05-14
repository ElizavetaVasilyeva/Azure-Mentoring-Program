using System.Threading.Tasks;
using AdventureWorks.Services.Production;
using Microsoft.Azure.Search.Models;
using Document = AdventureWorks.AzureSearch.Models.Document;

namespace AdventureWorks.AzureSearch
{
    public interface IAzureSearchService
    {
        Task<DocumentSearchResult<Product>> SearchProductsAsync(string searchTerm);
        Task<DocumentSearchResult<Document>> SearchDocsAsync(string searchTerm);
    }
}
