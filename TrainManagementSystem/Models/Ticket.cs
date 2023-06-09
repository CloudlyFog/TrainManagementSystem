﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TrainManagementSystem.Models;

[Table("Tickets")]
public class Ticket
{
    public static readonly Ticket Default = new()
    {
        Id = Guid.Empty,
        Price = -1,
    };
    public Guid? Id { get; set; } = Guid.NewGuid();
    public decimal Price { get; set; }
    public TicketDateTime? TicketDateTime { get; set; }
    public TicketAddress? TicketAddress { get; set; }

    public Guid? TrainId { get; set; }
    public Train? Train { get; set; }

    public Guid? UserId { get; set; }
    public TrainUser? User { get; set; }

    public static Ticket SetTicket(Ticket ticket, Train train, TrainUser user)
    {
        ticket.Train = train;
        ticket.TrainId = train.Id;

        ticket.User = user;
        ticket.UserId = user.ID;

        return ticket;
    }
}

[NotMapped]
public class TicketDateTime
{
    public Guid? Id { get; set; } = Guid.NewGuid();
    public DateTime? DepartureDate { get; set; }
    public DateTime? ArrivalDate { get; set; }
}

[NotMapped]
public class TicketAddress
{
    public Guid? Id { get; set; } = Guid.NewGuid();
    public string DepartureAddress { get; set; }
    public string ArrivalAddress { get; set; }
}

public enum SeatType
{
    OpenPlan,
    Coupe,
    Business,
}
