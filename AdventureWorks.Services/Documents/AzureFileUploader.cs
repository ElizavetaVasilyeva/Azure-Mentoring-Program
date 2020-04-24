using System;
using System.Threading.Tasks;
using AdventureWorks.Services.Configuration;
using AdventureWorks.Services.Interfaces;
using AdventureWorks.Services.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace AdventureWorks.Services.Documents
{
    public class AzureFileUploader : IFileUploader
    {
        private readonly CloudBlobClient _blobClient;
        private readonly CloudQueueClient _queueClient;

        public AzureFileUploader()
        {
            _blobClient = AzureStorageConfigurator.GetAccount().CreateCloudBlobClient();
            _queueClient = AzureStorageConfigurator.GetAccount().CreateCloudQueueClient();
        }

        public async Task UploadFile(string fileName, byte[] bytes)
        {
            var container = _blobClient.GetContainerReference("documents");
            var blobName = fileName + Guid.NewGuid();
            var blob = container.GetBlockBlobReference(blobName);
            blob.UploadFromByteArray(bytes, 0, bytes.Length);

            var message = new QueueItem
            {
                FileName = fileName,
                BlobName = blobName
            };
            var json = JsonConvert.SerializeObject(message, Formatting.Indented);
            await _queueClient.GetQueueReference("notifications")
                .AddMessageAsync(new CloudQueueMessage(json));
        }
    }
}
