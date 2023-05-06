using BankSystem7.Services.Configuration;
using Microsoft.EntityFrameworkCore;
using TrainManagementSystem.Models;

namespace TrainManagementSystem.Services.Configuration;

public class TrainManagementModelConfiguration : ModelConfiguration
{
    public TrainManagementModelConfiguration()
    {
    }
    public TrainManagementModelConfiguration(bool initializeAccess) : base(initializeAccess)
    {
    }

    public override void Invoke(ModelBuilder modelBuilder)
    {
        ConfigureTicket(modelBuilder);
        base.Invoke(modelBuilder);
    }

    private void ConfigureTicket(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrainUser>()
            .HasMany(user => user.Tickets)
            .WithOne(ticket => ticket.User);

        modelBuilder.Entity<Train>()
            .HasMany(train => train.Passengers)
            .WithOne(ticket => ticket.Train)
            .HasForeignKey(ticket => ticket.TrainId);
    }
}
