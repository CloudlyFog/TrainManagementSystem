using BankSystem7.Models;
using BankSystem7.Services.Configuration;
using BankSystem7.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using TrainManagementSystem.Data;
using TrainManagementSystem.Models;
using ZstdSharp.Unsafe;

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
        if (item is null || Exist(x => x.Id == item.Id))
            return ExceptionModel.OperationFailed;

        UpdateTracker(item, EntityState.Added);
        _trainContext.SaveChanges();
        return ExceptionModel.Ok;
    }

    public ExceptionModel Delete(Train item)
    {
        if (!FitsConditions(item))
            return ExceptionModel.EntityNotExist;

        UpdateTracker(item, EntityState.Deleted);
        _trainContext.SaveChanges();
        return ExceptionModel.Ok;
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
        if (!FitsConditions(item))
            return ExceptionModel.EntityNotExist;

        UpdateTracker(item, EntityState.Modified); 
        _trainContext.SaveChanges();
        return ExceptionModel.Ok;
    }

    private void UpdateTracker(Train item, EntityState state)
    {
        _trainContext.UpdateTracker(item, state, delegate
        {
            item.Passengers = null;
        }, _trainContext);
    }

    protected override void UpdateOptions(ConfigurationOptions options)
    {
        UpdateDatabaseName(options.DatabaseName);
        base.UpdateOptions(options);
    }
}
