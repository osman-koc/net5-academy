using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET5Academy.Services.PhotoStock.Application.Services;
using NET5Academy.Shared.ControllerBases;
using System.Threading;
using System.Threading.Tasks;

namespace NET5Academy.Services.PhotoStock.Application.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class PhotoController : OkBaseController
    {
        private readonly IPhotoService _photoService;
        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile photoFile, CancellationToken cancellationToken)
        {
            var response = await _photoService.SaveFile(photoFile, cancellationToken);
            return OkActionResult(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string name)
        {
            var response = await _photoService.DeleteFileByName(name);
            return OkActionResult(response);
        }
    }
}
