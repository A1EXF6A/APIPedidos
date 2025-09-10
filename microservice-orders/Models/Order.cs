using System.ComponentModel.DataAnnotations.Schema;

namespace microservice_orders.Models;

[Table("orders")]
public class Order
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("amount")]
    public int Amount { get; set; }
}
