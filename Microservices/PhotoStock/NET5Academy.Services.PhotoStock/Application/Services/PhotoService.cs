using Microsoft.AspNetCore.Http;
using NET5Academy.Services.PhotoStock.Application.Dtos;
using NET5Academy.Shared.Models;
using System.IO;
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
                return OkResponse<PhotoDto>.Error(System.Net.HttpStatusCode.BadRequest, "File cannot be null");
            }
            
            PhotoDto newFile = new(photoFile.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", newFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photoFile.CopyToAsync(stream, cancellationToken);
            }

            return OkResponse<PhotoDto>.Success(System.Net.HttpStatusCode.OK, newFile);
        }
    }
}
