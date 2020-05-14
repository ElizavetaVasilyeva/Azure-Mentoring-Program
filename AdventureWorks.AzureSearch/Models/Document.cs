using System;
using Newtonsoft.Json;

namespace AdventureWorks.AzureSearch.Models
{
    public class Document
    {
        [JsonProperty("metadata_storage_name")]
        public string StorageName { get; set; }

        [JsonProperty("metadata_storage_path")]
        public string StoragePath { get; set; }

        [JsonProperty("metadata_storage_last_modified")]
        public DateTime LastModified { get; set; }

        [JsonProperty("metadata_storage_content_type")]
        public string StorageContentType { get; set; }

        [JsonProperty("metadata_storage_size")]
        public long Size { get; set; }

        [JsonProperty("metadata_storage_file_extension")]
        public string Extension { get; set; }
    }
}
