using Microsoft.AspNetCore.Mvc;

namespace api_gateway.Controllers;

[ApiController]
[Route("gateway")]
public class GatewayController(IHttpClientFactory httpClientFactory) : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("http://localhost:5001/orders");
        var content = await response.Content.ReadAsStringAsync();
        return Content(content, "application/json");
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("http://localhost:5002/products");
        var content = await response.Content.ReadAsStringAsync();
        return Content(content, "application/json");
    }
}
