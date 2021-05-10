using Microsoft.AspNetCore.Http;
using NET5Academy.Services.PhotoStock.Application.Dtos;
using NET5Academy.Shared.Models;
using System.Threading;
using System.Threading.Tasks;

namespace NET5Academy.Services.PhotoStock.Application.Services
{
    public interface IPhotoService
    {
        Task<OkResponse<PhotoDto>> SaveFile(IFormFile photoFile, CancellationToken cancellationToken);
        Task<OkResponse<object>> DeleteFileByName(string fileName);
    }
}
