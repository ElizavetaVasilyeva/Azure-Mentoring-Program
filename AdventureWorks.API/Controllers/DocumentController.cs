using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AdventureWorks.Services.Interfaces;

namespace AdventureWorks.API.Controllers
{
    public class DocumentController : ApiController
    {
        private readonly IFileUploader _fileUploader;

        public DocumentController(IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
        }

        // POST: api/Document
        [HttpPost]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = await Request.Content.ReadAsMultipartAsync();
            var bytes = await provider.Contents.First().ReadAsByteArrayAsync();

            await _fileUploader.UploadFile(bytes);

            return Ok();
        }
    }
}