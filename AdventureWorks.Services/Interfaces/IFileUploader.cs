using System.Threading.Tasks;

namespace AdventureWorks.Services.Interfaces
{
    public interface IFileUploader
    {
        Task UploadFile(string fileName, byte[] bytes);
    }
}
