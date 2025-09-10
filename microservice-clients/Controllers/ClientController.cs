using Microsoft.AspNetCore.Mvc;

namespace microservice_clients.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IHttpClientFactory httpClientFactory) : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrdersFromOrderService()
    {
        var client = _httpClientFactory.CreateClient();
        var orders = await client.GetFromJsonAsync<IEnumerable<object>>("http://localhost:5001/api/orders");
        return Ok(orders);
    }
}
