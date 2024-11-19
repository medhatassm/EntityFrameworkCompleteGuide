using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreConfiguration.Entities;
// [Table("tblUsers")]
public class User
{
    public int UserId { get; set; }
    public string? Username { get; set; }
}