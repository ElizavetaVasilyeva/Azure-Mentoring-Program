using System;

namespace AdventureWorks.Services.Models
{
    [Serializable]
    public class QueueItem
    {
        public string FileName { get; set; }
        public string BlobName { get; set; }
    }
}
