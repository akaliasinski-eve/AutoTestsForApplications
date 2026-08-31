using AutoTestsForApplications.Interfaces.DapperInterfaces;
using AutoTestsForApplications.Interfaces.EmailSenderInterface;
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
        var user = await repo1.GetUserByFirstAndLastName("Елена", "Кузнецова");

        var repo2 = precondition.Provider.GetService<IAddressRepository>();
        var address = await repo2.GetAddressByUserId(user.Id);
        address.City.Should().Be("Казань");
    }

    //2.1 Получить из базы все категории, проверить на количество
    [Test]
    public async Task GetAllCategories()
    {
        var repo = precondition.Provider.GetService<ICategoryRepository>();
        var categories = await repo.GetAllCategoriesAsync();
        categories.Should().HaveCount(6);
    }

    //2.2 Получить из таблицы Products определенный продукт по его id,
    [Test]
    public async Task GetProductById()
    {
        var repo = precondition.Provider.GetService<IProductRepository>();
        var product = await repo.GetProductByIdAsync(2);
        product.Name.Should().Be("Samsung Galaxy S24");
        product.Description.Should().Be("Флагманский смартфон Samsung");
        product.Price.Should().Be(69990);
        product.Stock.Should().Be(20);
        product.CategoryId.Should().Be(1);
    }

    //2.3 Получить из таблицы Orders конкретный заказ конкретного юзера
    [Test]
    public async Task CheckItemsFromOrders()
    {
        var repo1 = precondition.Provider.GetService<IOrderItemRepository>();
        var orderItems = await repo1.GetOrderItemsByOrderIdAsync(11);
        var repo2 = precondition.Provider.GetService<IProductRepository>();
        var product1 = await repo2.GetProductByIdAsync(orderItems.ElementAt(0).ProductId);
        var product2 = await repo2.GetProductByIdAsync(orderItems.ElementAt(1).ProductId);

        product1.Name.Should().Be("AirPods Pro 2");
        product2.Name.Should().Be("Anker PowerBank");
    }
}