using AutoTestsForApplications.DTO.DapperDTOs;
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

    //2) Даппер: Проверить, что товары категории Аксессуары покупают пользователи, живущие в разных городах
    [Test]
    public async Task CheckAddressesOfUsersBuyingAccessories()
    {
        var categoryRepo = precondition.Provider.GetService<ICategoryRepository>();
        var category = await categoryRepo.GetCategoryByNameAsync("Аксессуары");
        var categoryId = category.Id;
        var productRepo = precondition.Provider.GetService<IProductRepository>();
        var products = await productRepo.GetProductsByCategoryId(categoryId);
        var orderItemsRepo = precondition.Provider.GetService<IOrderItemRepository>();
        var ordersRepo = precondition.Provider.GetService<IOrderRepository>();
        var usersRepo = precondition.Provider.GetService<IUserRepository>();
        var addressesRepo = precondition.Provider.GetService<IAddressRepository>();

        List<OrderItemDTO> orderItems = new();
        foreach (var product in products)
        {
            var foundOrderItems = await orderItemsRepo.GetOrderItemsByProductIdAsync(product.Id);
            var addList = foundOrderItems.ToList();
            orderItems.AddRange(addList);
        }

        List<OrderDTO> listOfOrders = new();
        foreach (var orderItem in orderItems)
        {
            var foundOrder = await ordersRepo.GetOrderByIdAsync(orderItem.OrderId);
            listOfOrders.Add(foundOrder);
        }

        List<UserDTO> listOfUsers = new();
        foreach (var order in listOfOrders)
        {
            var foundUser = await usersRepo.GetByIdAsync(order.UserId);
            listOfUsers.Add(foundUser);
        }

        List<AddressDTO> listOfAddresses = new();
        foreach (var user in listOfUsers)
        {
            var foundAddress = await addressesRepo.GetAddressByUserId(user.Id);
            listOfAddresses.Add(foundAddress);
        }

        List<AddressDTO> uniqueAddresses = listOfAddresses.DistinctBy(a => a.City).ToList();
        uniqueAddresses.Count.Should().BeGreaterThan(1);
    }

    //3) Даппер: Проверить, что покупатели телевизоров покупают также и аксессуары
    [Test]
    public async Task CheckBuyersOfTVs()
    {
        var categoryRepo = precondition.Provider.GetService<ICategoryRepository>();
        var category = await categoryRepo.GetCategoryByNameAsync("Телевизоры");
        var categoryId = category.Id;
        var productRepo = precondition.Provider.GetService<IProductRepository>();
        var products = await productRepo.GetProductsByCategoryId(categoryId);
        var orderItemsRepo = precondition.Provider.GetService<IOrderItemRepository>();
        var ordersRepo = precondition.Provider.GetService<IOrderRepository>();
        var usersRepo = precondition.Provider.GetService<IUserRepository>();

        List<OrderItemDTO> orderItemsTV = new();
        foreach (var product in products)
        {
            var foundOrderItems = await orderItemsRepo.GetOrderItemsByProductIdAsync(product.Id);
            var addList = foundOrderItems.ToList();
            orderItemsTV.AddRange(addList);
        }

        List<OrderDTO> listOfOrdersTV = new();
        foreach (var orderItem in orderItemsTV)
        {
            var foundOrder = await ordersRepo.GetOrderByIdAsync(orderItem.OrderId);
            listOfOrdersTV.Add(foundOrder);
        }

        List<UserDTO> listOfUsersTV = new();
        foreach (var order in listOfOrdersTV)
        {
            var foundUser = await usersRepo.GetByIdAsync(order.UserId);
            listOfUsersTV.Add(foundUser);
        }
        List<OrderDTO> listOfOrdersOfTVUsers = new();
        foreach (var user in listOfUsersTV)
        {
            var foundOrders = await ordersRepo.GetOrdersByUserIdAsync(user.Id);
            var addList = foundOrders.ToList();
            listOfOrdersOfTVUsers.AddRange(addList);
        }
        
        List<OrderItemDTO> orderItemsOfTVUsers = new();
        foreach (var order in listOfOrdersOfTVUsers)
        {
            var foundOrderItems = await orderItemsRepo.GetOrderItemsByOrderIdAsync(order.Id);
            var addList = foundOrderItems.ToList();
            orderItemsOfTVUsers.AddRange(addList);
        }
        
        List<ProductDTO> productsOfTVUsers = new();
        foreach (var orderItem in orderItemsOfTVUsers)
        {
            var foundProduct = await productRepo.GetProductByIdAsync(orderItem.ProductId);
            productsOfTVUsers.Add(foundProduct);
        }
        
        var categoryAccessories = await categoryRepo.GetCategoryByNameAsync("Аксессуары");
        var categoryAccessoriesId = categoryAccessories.Id;
        var listOfaccessories = productsOfTVUsers.Where(p => p.CategoryId == categoryAccessoriesId).ToList();
        listOfaccessories.Count.Should().BeGreaterThan(0);
    }
}