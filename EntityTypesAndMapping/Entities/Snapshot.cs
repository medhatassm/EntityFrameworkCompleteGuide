using System.ComponentModel.DataAnnotations.Schema;

namespace EntityTypesAndMapping.Entities;

// [NotMapped]
public class Snapshot
{
    public DateTime LoadedAt => DateTime.Now;
    public String Version => Guid.NewGuid().ToString().Substring(0, 8);
}