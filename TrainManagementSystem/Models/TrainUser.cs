using BankSystem7.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainManagementSystem.Models;

[Table("Users")]
public class TrainUser : User
{
    public List<Ticket> Tickets { get; set; } = new();
}
