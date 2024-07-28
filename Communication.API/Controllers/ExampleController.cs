using Communication.Domain.Products;
using Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace Communication.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExampleController(IGenericRepository<Product> productRepository) : ControllerBase
{
    //write  product crud operation
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await productRepository.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        await productRepository.Insert(new Product { Name = "kalem 1" });
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return Ok(await productRepository.GetById(id));
    }

    [HttpPut]
    public async Task<IActionResult> Put(Product product)
    {
        await productRepository.Update(product);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await productRepository.Delete(id);
        return Ok();
    }
}