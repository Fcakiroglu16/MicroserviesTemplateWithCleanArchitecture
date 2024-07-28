using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Auth.API.Controllers;

[ApiVersion(2)]
[ApiVersion(1)]
[Route("api/[controller]/[action]")]
[ApiController]
public class ExampleController(IDistributedCache distributedCache, ILogger<ExampleController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetOrSetRedisTest()
    {
        logger.LogInformation("GetOrSetRedisTest method çalıştı");

        await distributedCache.SetStringAsync("version", "v1");

        var version = await distributedCache.GetStringAsync("version");

        return Ok(version);
    }


    [HttpGet("/GetList")]
    [MapToApiVersion(1)]
    public IActionResult GetList()
    {
        return Ok("1. version");
    }


    [HttpGet("/GetList")]
    [MapToApiVersion(2)]
    public IActionResult GetListV2()
    {
        return Ok("2. version");
    }
}