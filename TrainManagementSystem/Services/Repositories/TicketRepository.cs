using Amazon.SecurityToken.Model.Internal.MarshallTransformations;
using BankSystem7.Models;
using BankSystem7.Services.Configuration;
using BankSystem7.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainManagementSystem.Data;
using TrainManagementSystem.Models;

namespace TrainManagementSystem.Services.Repositories;

public class TicketRepository : OptionsUpdater<TrainUser, Card, BankAccount, Bank, Credit>, IRepository<Ticket>
{
    private readonly TrainContext _trainContext;
    private bool _disposed;
    public TicketRepository(ConfigurationOptions options)
    {
        UpdateOptions(options);
        _trainContext = new TrainContext();
    }
    public IQueryable<Ticket> All =>
        _trainContext.Tickets
        .Include(x => x.Train)
        .Include(x => x.User) ?? Enumerable.Empty<Ticket>().AsQueryable();

    public ExceptionModel Create(Ticket item)
    {
        if (item?.User is null && item?.Train is null)
            return ExceptionModel.EntityIsNull;

        if (Exist(x => x.Id == item.Id))
            return ExceptionModel.OperationRestricted;

        UpdateTracker(item, EntityState.Added);
        _trainContext.SaveChanges();
        return ExceptionModel.Ok;
    }

    public ExceptionModel Delete(Ticket item)
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

    public bool Exist(Expression<Func<Ticket, bool>> predicate)
    {
        return All.Any(predicate);
    }

    public bool FitsConditions(Ticket? item)
    {
        return item?.User is not null && item?.Train is not null && Exist(x => x.Id == item.Id);
    }

    public Ticket Get(Expression<Func<Ticket, bool>> predicate)
    {
        return All.FirstOrDefault(predicate) ?? Ticket.Default;
    }

    public ExceptionModel Update(Ticket item)
    {
        if (!FitsConditions(item))
            return ExceptionModel.EntityNotExist;

        UpdateTracker(item, EntityState.Modified);
        _trainContext.SaveChanges();
        return ExceptionModel.Ok;
    }

    private void UpdateTracker(Ticket item, EntityState state)
    {
        _trainContext.UpdateTracker(item, state, delegate
        {
            _trainContext.AvoidChanges(new object[] { item.Train, item.User });
        }, _trainContext);
    }

    protected override void UpdateOptions(ConfigurationOptions options)
    {
        UpdateDatabaseName(options.DatabaseName);
        base.UpdateOptions(options);
    }
}
