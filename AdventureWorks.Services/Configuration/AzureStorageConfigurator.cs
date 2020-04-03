using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Serilog;

namespace AdventureWorks.Services.Configuration
{
    public static class AzureStorageConfigurator
    {
        public static ILogger GetLogger()
        {
            var storage = GetStorage();
            return new LoggerConfiguration()
                .WriteTo.AzureTableStorage(storage)
                .CreateLogger();
        }

        public static CloudStorageAccount GetStorage()
        {
            var connection = CloudConfigurationManager.GetSetting("StorageConnectionString");
            return CloudStorageAccount.Parse(connection);
        }

        public static CloudStorageAccount GetAccount()
        {
            var connection = CloudConfigurationManager.GetSetting("AccountConnectionString");
            return CloudStorageAccount.Parse(connection);
        }
    }
}