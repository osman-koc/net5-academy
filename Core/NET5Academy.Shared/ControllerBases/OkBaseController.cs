using Microsoft.AspNetCore.Mvc;
using NET5Academy.Shared.Models;
using System.Linq;

namespace NET5Academy.Shared.ControllerBases
{
    public class OkBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(OkResponse<T> response)
        {
            if (response.IsSuccess)
            {
                return new ObjectResult(response.Data) { StatusCode = (int)response.StatusCode };
            }
            else
            {
                return new ObjectResult(response.Errors?.FirstOrDefault()) { StatusCode = (int)response.StatusCode };
            }
        }
    }
}
