using BankSystem7.Services.Configuration;
using BankSystem7.Services.Interfaces;
using TrainManagementSystem.Models;
using TrainManagementSystem.Services.Repositories;

namespace TrainManagementSystem.Extensions;

public static class TrainManagementExtensions
{
    public static IServiceCollection AddTrainManagementSystem(this IServiceCollection services, Action<ConfigurationOptions> options)
    {
        var resultOptions = new ConfigurationOptions();
        options.Invoke(resultOptions);
        services.AddSingleton<IRepository<Train>>(new TrainRepository(resultOptions));
        services.AddSingleton<IRepository<Ticket>>(new TicketRepository(resultOptions));
        return services;
    }

    public static IServiceCollection AddTrainManagementSystem(this IServiceCollection services, ConfigurationOptions options)
    {
        services.AddSingleton<IRepository<Train>>(new TrainRepository(options));
        services.AddSingleton<IRepository<Ticket>>(new TicketRepository(options));
        return services;
    }
}
