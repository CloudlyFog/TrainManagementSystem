using System.ComponentModel.DataAnnotations.Schema;

namespace TrainManagementSystem.Models;

[Table("Trains")]
public class Train
{
    public static readonly Train Default = new()
    {
        Id = Guid.Empty,
        SeatsCount = -1,
        CarriageCount = -1,
    };
    public Guid? Id { get; set; } = Guid.NewGuid();
    public int SeatsCount { get; set; }
    public int CarriageCount { get; set; }
    public TrainModel TrainModel { get; set; }
    public List<Ticket>? Passengers { get; set; } = new();
}

public enum TrainModel
{
    Intercity,
    Interregional,
    International,
}
