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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
    try
    {
        db.Database.ExecuteSqlRaw("SELECT 1");
        Console.WriteLine("Conexión a la DB exitosa.");
    }
    catch (Exception ex)
    {
        throw new InvalidOperationException("No se pudo conectar a la DB:", ex);
    }
}

app.MapControllers();
app.Run();
