using System.Threading.Tasks;
using System.Web.Mvc;
using AdventureWorks.AzureSearch;

namespace AdventureWorks.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IAzureSearchService _azureSearchService;

        public SearchController(IAzureSearchService azureSearchService)
        {
            _azureSearchService = azureSearchService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> SearchProducts(string searchTerm)
        {
            var model = await _azureSearchService.SearchProductsAsync(searchTerm);

            return PartialView(model: model);
        }

        [HttpGet]
        public async Task<ActionResult> SearchDocuments(string searchTerm)
        {
            var model = await _azureSearchService.SearchDocsAsync(searchTerm);

            return PartialView(model: model);
        }
    }
}