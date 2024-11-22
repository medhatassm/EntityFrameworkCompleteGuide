using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityTypesAndMapping.Entities;

// [Table("Orders" , Schema = "Sales")]
public class Order
{
    // [Key]
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string? CustomerEmail { get; set; }
    public List<OrderDetails> OrderDetailsList { get; set; } = new List<OrderDetails>();
}