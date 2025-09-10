using Microsoft.EntityFrameworkCore;

namespace microservice_orders.Models;

public class OrdersDbContext(DbContextOptions<OrdersDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
}
