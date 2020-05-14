using System;
using System.Threading.Tasks;
using AdventureWorks.Services;
using AdventureWorks.Services.Production;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Serilog;
using Document = AdventureWorks.AzureSearch.Models.Document;

namespace AdventureWorks.AzureSearch
{
    public class AzureSearchService : IAzureSearchService
    {
        private readonly ILogger _log;
        private readonly string _serviceClientName;
        private readonly string _serviceClientKey;

        public AzureSearchService(ILogger log)
        {
            _log = log;
            _serviceClientName = KeyVaultService.ServiceSearchClientName;
            _serviceClientKey = KeyVaultService.ServiceSearchClientKey;
        }

        public async Task<DocumentSearchResult<Product>> SearchProductsAsync(string searchTerm)
        {
            try
            {
                SearchParameters parameters = new SearchParameters
                {
                    Select = new[] { "*" },
                    IncludeTotalResultCount = true
                };

                SearchIndexClient searchIndexClient = new SearchIndexClient(_serviceClientName, "azuresql-index", new SearchCredentials(_serviceClientKey));

                return await searchIndexClient.Documents.SearchAsync<Product>(searchTerm, parameters);
            }
            catch (Exception e)
            {
                _log.Error(e, "Exception while searching products");
                throw;
            }
        }

        public async Task<DocumentSearchResult<Document>> SearchDocsAsync(string searchTerm)
        {
            try
            {
                SearchParameters parameters = new SearchParameters
                {
                    Select = new[] { "metadata_storage_name, metadata_storage_path, metadata_storage_last_modified, metadata_storage_content_type, metadata_storage_size, metadata_storage_file_extension" }
                };

                SearchIndexClient searchIndexClient = new SearchIndexClient(_serviceClientName, "azureblob-index", new SearchCredentials(_serviceClientKey));

                return await searchIndexClient.Documents.SearchAsync<Document>(searchTerm, parameters);
            }
            catch (Exception e)
            {
                _log.Error(e, "Exception while searching documents");
                throw;
            }
        }
    }
}
