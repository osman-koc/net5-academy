using Microsoft.AspNetCore.Mvc;
using NET5Academy.Shared.ControllerBases;
using NET5Academy.Shared.Models;
using System.Threading.Tasks;

namespace NET5Academy.Services.Payment.Application.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class FakePaymentController : OkBaseController
    {
        /// <summary>
        /// Receive payment (it's fake, returns true)
        /// </summary>
        /// <returns>true</returns>
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var response = OkResponse<bool>.Success(System.Net.HttpStatusCode.Created, true);
            return OkActionResult(response);
        }
    }
}
