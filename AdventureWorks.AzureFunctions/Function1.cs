using System.IO;
using AdventureWorks.Services.Models;
using AdventureWorks.Services.Production;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace AdventureWorks.AzureFunctions
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async void Run([QueueTrigger("notifications", Connection = "queue")]QueueItem queueItem,
            [Blob("documents/{BlobName}", FileAccess.Read)]Stream file,
            TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {queueItem.FileName} + {file}");

            if (file != null)
            {
                using (var br = new BinaryReader(file))
                {
                    byte[] fileContent = br.ReadBytes((int)file.Length);

                    IProductDocumentService repository = new ProductDocumentService();
                    repository.AddProductDocument(new ProductDocument
                    {
                        FileBytes = fileContent,
                        FileName = queueItem.FileName
                    });
                }
            }
        }
    }
}
