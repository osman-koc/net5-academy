using Microsoft.AspNetCore.Mvc;
using NET5Academy.Shared.Models;

namespace NET5Academy.Shared.Controllers
{
    public class OkBaseController : ControllerBase
    {
        public IActionResult OkActionResult<T>(OkResponse<T> response)
        {
            return new ObjectResult(response) { StatusCode = (int)response.StatusCode };
        }
    }
}
