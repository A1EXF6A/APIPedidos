using Microsoft.EntityFrameworkCore;
using microservice_orders.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var conn = builder.Configuration.GetConnectionString("PostgreSQLConnection")
    ?? throw new InvalidOperationException("La cadena de conexión para PostgreSQL no está configurada.");

builder.Services.AddDbContext<OrdersDbContext>(options =>
{
    options.UseNpgsql(conn);
});

var app = builder.Build();

app.MapControllers();
app.Run();
