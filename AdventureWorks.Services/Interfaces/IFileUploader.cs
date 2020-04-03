using System.Threading.Tasks;

namespace AdventureWorks.Services.Interfaces
{
    public interface IFileUploader
    {
        Task UploadFile(byte[] bytes);
    }
}
