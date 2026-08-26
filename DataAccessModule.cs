using AutoTestsForApplications.DapperRepository;
using AutoTestsForApplications.Interfaces.DapperInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestsForApplications;

public static class DataAccessModule
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUserRepository>(p => new UserRepository(connectionString));
        services.AddScoped<IAddressRepository>(p => new AddressRepository(connectionString));
        return services;
    }
}