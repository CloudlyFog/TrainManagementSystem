using BankSystem7.AppContext;
using BankSystem7.Models;
using BankSystem7.Services.Configuration;
using Microsoft.EntityFrameworkCore;
using TrainManagementSystem.Models;

namespace TrainManagementSystem.Data;

public class TrainContext : GenericDbContext<TrainUser, Card, BankAccount, Bank, Credit>
{
    public TrainContext()
    {
    }

    public TrainContext(ModelConfiguration? modelConfiguration) : base(modelConfiguration)
    {
    }

    public DbSet<Train> Trains { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
}
