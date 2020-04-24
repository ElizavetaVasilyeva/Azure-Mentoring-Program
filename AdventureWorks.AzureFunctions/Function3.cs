using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AdventureWorks.Services.Documents;
using AdventureWorks.Services.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace AdventureWorks.AzureFunctions
{
    public static class Function3
    {
        [FunctionName("Function3")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            if (!req.Content.IsMimeMultipartContent())
            {
                return req.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = await req.Content.ReadAsMultipartAsync();
            var bytes = await provider.Contents.First().ReadAsByteArrayAsync();
            var fileName = Path.GetFileName(provider.Contents.First().Headers.ContentDisposition.FileName.Replace("\"", string.Empty));

            IFileUploader azureFileUploader = new AzureFileUploader();
            await azureFileUploader.UploadFile(fileName, bytes);

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
