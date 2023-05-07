using BankSystem7.Models;
using BankSystem7.Services.Configuration;
using Microsoft.EntityFrameworkCore;
using TrainManagementSystem.Data;
using TrainManagementSystem.Models;
using TrainManagementSystem.Services.Configuration;
using TrainManagementSystem.Services.Repositories;

namespace TrainManagementSystemTest;

public class Tests
{
    private TrainRepository _trainRepository;
    private TicketRepository _ticketRepository; 
    private ServiceConfiguration<TrainUser, Card, BankAccount, Bank, Credit> _serviceConfiguration =
        ServiceConfiguration<TrainUser, Card, BankAccount, Bank, Credit>.CreateInstance(GetOptions());

    [SetUp]
    public void Setup()
    {
        _trainRepository = new TrainRepository(GetOptions());
        _ticketRepository = new TicketRepository(GetOptions());
    }

    [Test]
    public void TestTrainRepository()
    {

        var train = GetDefaultTrain();
        _trainRepository.Create(train);
        var getTrain = _trainRepository.Get(x => x.Id == train.Id);
        getTrain.SeatsCount = 23423;
        _trainRepository.Update(getTrain);
        _trainRepository.Delete(getTrain);
        Assert.Pass();
    }

    [Test]
    public void TestTicketRepository()
    {
        var ticket = GetTicket();
        _serviceConfiguration.UserRepository.Create(GetUser());

        _ticketRepository.Create(ticket);
        var updTicket = _ticketRepository.Get(x => x.Id == ticket.Id);
        updTicket.Price = 234234;
        _ticketRepository.Update(updTicket);
        _ticketRepository.Delete(updTicket);
        Assert.Pass();
    }

    private Train GetDefaultTrain()
    {
        return new Train()
        {
            Id = new Guid("F0B50C2A-1984-4ED5-BF2A-3FF17FEF8E8D"),
            SeatsCount = 1000,
            CarriageCount = 20,
            TrainModel = TrainModel.Interregional,
        };
    }

    private TrainUser GetDefaultUser()
    {
        return new TrainUser(User.Default)
        {

        };
    }

    private TrainUser GetUser()
    {
        var bank = new Bank
        {
            ID = new Guid("376BAC1B-0CC2-4CCE-B10A-D21570CA3736"),
            BankName = "Tinkoff",
        };

        var bank2 = new Bank()
        {
            ID = new Guid("5A3DBB2C-33D9-45E7-B108-C99928E488B7"),
            BankName = "AlphaBank",
        };

        var user = new TrainUser()
        {
            ID = new Guid("4AB6414C-AD4C-4094-BC91-869E0CF65429"),
            Name = "alex",
            Email = "alex@gmail.com",
            Password = "test",
            PhoneNumber = "1613750023",
            Age = 17
        };
        var bankAccount = new BankAccount(user, bank)
        {
            BankAccountAmount = 10_000,
        };

        user.Card = new Card(user.Age, user: user, bankAccount: bankAccount);

        return user;
    }

    private Ticket GetTicket()
    {
        return Ticket.SetTicket(GetDefaultTicket(), GetDefaultTrain(), GetUser());
    }

    private Ticket GetDefaultTicket()
    {
        return new Ticket
        {
            Price = 1000,
            TicketDateTime = new TicketDateTime()
            {
                DepartureDate = DateTime.Now.AddDays(2),
                ArrivalDate = DateTime.Now.AddDays(3),
            },
            TicketAddress = new TicketAddress()
            {
                DepartureAddress = "dpAddress",
                ArrivalAddress = "arrAddress"
            },
        };
    }
 
    private static ConfigurationOptions GetOptions()
    {
        return new ConfigurationOptions()
        {
            DatabaseName = "Test2",
            EnsureCreated = false,
            EnsureDeleted = false,
            Contexts = new Dictionary<DbContext, ModelConfiguration?>
            {
                { new TrainContext(), new TrainManagementModelConfiguration(true) }
            },
        };
    }
}