using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AdventureWorks.Services.Production;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace AdventureWorks.AzureFunctions
{
    public static class Function2
    {
        [FunctionName("Function2")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a get request.");

            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "fileName", true) == 0)
                .Value;
            string guid = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "guid", true) == 0)
                .Value;

            if (name == null && guid == null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a fileName and guid on the query string");
            }

            IProductDocumentService repository = new ProductDocumentService();
            var file = repository.GetFile(name, guid);

            if (file == null)
            {
                return req.CreateResponse(HttpStatusCode.NotFound, "Document with provided fileName and guid doesn't exist");
            }

            HttpResponseMessage response = req.CreateResponse(HttpStatusCode.OK);
            response.Content = new StreamContent(new MemoryStream(file.FileBytes));
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = file.FileName
            };

            return response;
        }
    }
}
