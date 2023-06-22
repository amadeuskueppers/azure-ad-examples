using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]s")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        var results = Enumerable.Range(1, 5).Select(index => new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product Name " + index
            })
            .ToArray();
        return Ok(results);
    }
}

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
