using Microsoft.AspNetCore.Http;
using NET5Academy.Services.PhotoStock.Application.Dtos;
using NET5Academy.Shared.Models;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NET5Academy.Services.PhotoStock.Application.Services
{
    public class PhotoService : IPhotoService
    {
        public async Task<OkResponse<PhotoDto>> SaveFile(IFormFile photoFile, CancellationToken cancellationToken)
        {
            if (photoFile == null || photoFile.Length <= 0 || string.IsNullOrEmpty(photoFile.FileName))
            {
                return OkResponse<PhotoDto>.Error(HttpStatusCode.BadRequest, "File cannot be null!");
            }
            
            PhotoDto newFile = new(photoFile.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", newFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photoFile.CopyToAsync(stream, cancellationToken);
            }

            return OkResponse<PhotoDto>.Success(HttpStatusCode.OK, newFile);
        }

        public async Task<OkResponse<object>> DeleteFileByName(string filePath)
        {
            if(string.IsNullOrEmpty(filePath))
            {
                return OkResponse<object>.Error(HttpStatusCode.BadRequest, "File path cannot be null!");
            }

            var existFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", filePath);
            if (string.IsNullOrEmpty(existFile) || !File.Exists(existFile))
            {
                return OkResponse<object>.Error(HttpStatusCode.NotFound, "Photo not found!");
            }

            File.Delete(existFile);
            return OkResponse<object>.Success(HttpStatusCode.NoContent);
        }
    }
}
