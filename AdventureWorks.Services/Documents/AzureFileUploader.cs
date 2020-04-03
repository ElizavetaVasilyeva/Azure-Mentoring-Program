using System;
using System.Threading.Tasks;
using AdventureWorks.Services.Configuration;
using AdventureWorks.Services.Interfaces;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;

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

        public async Task UploadFile(byte[] bytes)
        {
            var container = _blobClient.GetContainerReference("documents");
            var filename = $"{DateTime.Now:s}_{Guid.NewGuid()}.docx";
            var blob = container.GetBlockBlobReference(filename);
            blob.UploadFromByteArray(bytes, 0, bytes.Length);

            await _queueClient.GetQueueReference("notifications")
                .AddMessageAsync(new CloudQueueMessage($"File {filename} was uploaded"));
        }
    }
}
