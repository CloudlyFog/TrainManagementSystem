namespace TrainManagementSystem.Models;

public class Train
{
    public Guid? Id { get; set; } = Guid.NewGuid();
    public int SeatsCount { get; set; }
    public int CarriageCount { get; set; }
    public bool Arrived { get; set; }
    public TrainModel TrainModel { get; set; }
    public List<Ticket>? Passengers { get; set; } = new();
}

public enum TrainModel
{

}
