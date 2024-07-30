using Core.ResultDto;
using Microsoft.AspNetCore.Mvc;

namespace Core;

[Route("api/[controller]")]
[ApiController]
public class CustomControllerBase : ControllerBase
{
    [NonAction]
    public ObjectResult CreateResult<T>(ServiceResult<T> result)
    {
        return new ObjectResult(result) { StatusCode = result.Status.GetHashCode() };
    }

    [NonAction]
    public ObjectResult CreateResult(ServiceResult result)
    {
        return new ObjectResult(result) { StatusCode = result.Status.GetHashCode() };
    }
}