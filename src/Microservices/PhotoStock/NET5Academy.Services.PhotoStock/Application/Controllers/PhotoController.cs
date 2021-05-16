using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET5Academy.Services.PhotoStock.Application.Services;
using NET5Academy.Shared.Controllers;
using System.Threading;
using System.Threading.Tasks;

namespace NET5Academy.Services.PhotoStock.Application.Controllers
{
    [Route("api/v1/[controllers]")]
    [ApiController]
    public class PhotoController : OkBaseController
    {
        private readonly IPhotoService _photoService;
        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        /// <summary>
        /// Save photo file
        /// </summary>
        /// <param name="photoFile">IFormFile</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>PhotoDto</returns>
        [HttpPost]
        public async Task<IActionResult> Create(IFormFile photoFile, CancellationToken cancellationToken)
        {
            var response = await _photoService.SaveFile(photoFile, cancellationToken);
            return OkActionResult(response);
        }

        /// <summary>
        /// Delete photo file by name
        /// </summary>
        /// <param name="name">Photo Name</param>
        /// <returns>NoContent</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(string name)
        {
            var response = await _photoService.DeleteFileByName(name);
            return OkActionResult(response);
        }
    }
}
