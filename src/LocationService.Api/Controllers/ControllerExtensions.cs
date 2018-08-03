using LocationService.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationService.Api.Controllers
{
    public class ControllerExtensions : Controller
    {

        protected IActionResult ReturnResult<T>(Result<T> result)
        {
            switch (result.Status)
            {
                case ResultCode.OK:
                    if (result.Value == null)
                        return new StatusCodeResult(204);
                    return new OkObjectResult(result.ValueType);

                case ResultCode.BadRequest:
                    return new BadRequestObjectResult(result.Value);
                case ResultCode.NotFound:
                    return new NotFoundObjectResult(result.Value);
                case ResultCode.ServiceUnavailable:
                    return new StatusCodeResult(503);
                case ResultCode.BadGateway:
                    return new StatusCodeResult(502);
                case ResultCode.Error:
                default:
                    return new StatusCodeResult(500);
            }
        }

    }
}
