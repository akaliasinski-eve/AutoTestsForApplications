using AutoTestsForApplications.Interfaces.DapperInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using FluentAssertions;

namespace AutoTestsForApplications;

public class DapperTest
{
    private readonly TestPrecondition precondition = new();
   // [Test]
    public async Task Initialize()
    {
        var connectionString = "Data Source=marketplace.db";
        await using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();
        await DatabaseInitializer.InitializeAsync(connection);
    }

    [Test]
    public async Task GetAllUsers()
    {
        var repo = precondition.Provider.GetService<IUserRepository>();
        var users = await repo.GetAllAsync();
        users.Should().HaveCount(15);
    }

    [Test]
    public async Task GetUserById()
    {
        var repo = precondition.Provider.GetService<IUserRepository>();
        var user = await repo.GetByIdAsync(10);
        user.Should().NotBeNull();
    }
    
    [Test]
    public async Task GetAllAddresses()
    {
        var repo = precondition.Provider.GetService<IAddressRepository>();
        var addresses = await repo.GetAllAddressesAsync();
        addresses.Should().HaveCount(15);
    }

    [Test]
    public async Task GetAddressById()
    {
        var repo = precondition.Provider.GetService<IAddressRepository>();
        var address = await repo.GetAddressByUserId(10);
        address.Should().NotBeNull();
    }
    
    [Test]
    public async Task GetUserByFirstAndLastName()
    {
        var repo1 = precondition.Provider.GetService<IUserRepository>();
        var user = await repo1.GetUserByFirstAndLastName("Елена","Кузнецова");
        
        var repo2 = precondition.Provider.GetService<IAddressRepository>();
        var address = await repo2.GetAddressByUserId(user.Id);
        address.City.Should().Be("Казань");
    }
}