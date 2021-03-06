using Microsoft.AspNetCore.Mvc;
using NET5Academy.Shared.Controllers;
using NET5Academy.Shared.Models;
using System.Threading.Tasks;

namespace NET5Academy.Services.Payment.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FakePaymentController : OkBaseController
    {
        /// <summary>
        /// Receive payment (it's fake, returns true)
        /// </summary>
        /// <returns>Success (bool)</returns>
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var response = OkResponse<bool>.Success(System.Net.HttpStatusCode.Created, true);
            return OkActionResult(response);
        }
    }
}
