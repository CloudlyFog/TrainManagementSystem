using BankSystem7.Models;
using BankSystem7.Services.Configuration;
using BankSystem7.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainManagementSystem.Data;
using TrainManagementSystem.Models;

namespace TrainManagementSystem.Services.Repositories;

public class TrainRepository : OptionsUpdater<TrainUser, Card, BankAccount, Bank, Credit>, IRepository<Train>
{
    private readonly TrainContext _trainContext;
    private bool _disposed;
    public TrainRepository(ConfigurationOptions options)
    {
        UpdateOptions(options);
        _trainContext = new TrainContext();
    }
    public IQueryable<Train> All =>
        _trainContext.Trains
        .Include(x => x.Passengers)
        .ThenInclude(x => x.User) ?? Enumerable.Empty<Train>().AsQueryable();

    public ExceptionModel Create(Train item)
    {
        throw new NotImplementedException();
    }

    public ExceptionModel Delete(Train item)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _trainContext.Dispose();
        _disposed = true;
    }

    public bool Exist(Expression<Func<Train, bool>> predicate)
    {
        return All.Any(predicate);
    }

    public bool FitsConditions(Train? item)
    {
        return item is not null && Exist(x => x.Id == item.Id);
    }

    public Train Get(Expression<Func<Train, bool>> predicate)
    {
        return All.FirstOrDefault(predicate) ?? Train.Default;
    }

    public ExceptionModel Update(Train item)
    {
        throw new NotImplementedException();
    }

    protected override void UpdateOptions(ConfigurationOptions options)
    {
        UpdateDatabaseName(options.DatabaseName);
        base.UpdateOptions(options);
    }
}
