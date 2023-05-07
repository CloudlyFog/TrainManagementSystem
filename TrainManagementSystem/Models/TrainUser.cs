using BankSystem7.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainManagementSystem.Models;

[Table("Users")]
public class TrainUser : User
{
    public TrainUser()
    {
        
    }
    public TrainUser(User user)
    {
        foreach (var prop in user.GetType().GetProperties())
            user.GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(user));
    }
    public List<Ticket> Tickets { get; set; } = new();
}
