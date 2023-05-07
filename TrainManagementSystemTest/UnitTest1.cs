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
    [SetUp]
    public void Setup()
    {
        _trainRepository = new TrainRepository(GetOptions());
    }

    [Test]
    public void TestTrainRepository()
    {

        var train = GetTrain();
        _trainRepository.Create(train);
        var getTrain = _trainRepository.Get(x => x.Id == train.Id);
        getTrain.SeatsCount = 23423;
        _trainRepository.Update(getTrain);
        _trainRepository.Delete(getTrain);
        Assert.Pass();
    }

    private Train GetTrain()
    {
        return new Train()
        {
            Id = new Guid("F0B50C2A-1984-4ED5-BF2A-3FF17FEF8E8D"),
            SeatsCount = 1000,
            CarriageCount = 20,
            TrainModel = TrainModel.Interregional,
        };
    }
 
    private static ConfigurationOptions GetOptions()
    {
        return new ConfigurationOptions()
        {
            DatabaseName = "Test2",
            EnsureCreated = true,
            EnsureDeleted = true,
            Contexts = new Dictionary<DbContext, ModelConfiguration?>
            {
                { new TrainContext(), new TrainManagementModelConfiguration(true) }
            },
        };
    }
}