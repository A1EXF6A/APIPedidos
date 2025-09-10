using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using microservice_orders.Models;

namespace microservice_orders.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(OrdersDbContext db) : ControllerBase
{
    private readonly OrdersDbContext _db = db;

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order pedido)
    {
        _db.Orders.Add(pedido);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOrder), new { id = pedido.Id }, pedido);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var pedido = await _db.Orders.FindAsync(id);
        if (pedido == null) return NotFound();
        return Ok(pedido);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        var orders = await _db.Orders.ToListAsync();
        return Ok(orders);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order pedidoActualizado)
    {
        var pedido = await _db.Orders.FindAsync(id);
        if (pedido == null) return NotFound();

        pedido.Name = pedidoActualizado.Name;
        pedido.Amount = pedidoActualizado.Amount;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var pedido = await _db.Orders.FindAsync(id);
        if (pedido == null) return NotFound();

        _db.Orders.Remove(pedido);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
