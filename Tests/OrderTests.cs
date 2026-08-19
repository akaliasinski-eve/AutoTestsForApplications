using System.Text.Json;
using AutoTestsForApplications.DTO.OrderDTOs;
using FluentAssertions;
using FluentAssertions.Execution;

namespace AutoTestsForApplications;

public class OrderTests
{
    private OrderDataDTO order;

    [OneTimeSetUp]
    public void Setup()
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "OrderData.json");
        string data = File.ReadAllText(path);
        order = JsonSerializer.Deserialize<OrderDataDTO>(data);
    }

    [Test]
    public void Test1()
    {
        foreach (var item in order.Items)
        {
            TestContext.WriteLine($"{item.ProductId} | {item.Quantity} | {item.Price}");
        }

        order.Items.Should().NotBeEmpty();
        order.Items.Should().HaveCount(3);
    }

    [Test]
    public void Test2()
    {
        var sum = order.Items.Select(x => x.Quantity * x.Price).Sum();
        var expectedSum = order.Summary.ItemsTotal;
        sum.Should().Be(expectedSum);
    }

    [Test]
    public void Test3()
    {
        var listOfItems = order.Items.Where(x => x.Category == "Electronics").ToList();
        foreach (var item in listOfItems)
        {
            TestContext.WriteLine($"Electronics: {item.Name}");
        }

        listOfItems.Should().OnlyContain(x => x.Category == "Electronics");
    }

    [Test]
    public void CheckPaymentStatusIsPaid()
    {
        order.Payment.Status.Should().Be("paid");
    }

    [Test]
    public void Test4()
    {
        ItemDTO? mostExpensiveItem;
        mostExpensiveItem = order.Items.OrderByDescending(x => x.Price).First();
        /*      using (new AssertionScope())
              {
                  mostExpensiveItem.Price.Should().Be(129.99m);
                  mostExpensiveItem.Name.Should().Be("Wireless Headphones1");
              }
        */
        //mostExpensiveItem.Price.Should().Be(129.99m).And.mostExpensiveItem.Name.Should().Be("Wireless Headphones1");
        
        //исполюзуются анонимные типы
        mostExpensiveItem.Should().BeEquivalentTo(new 
        {
            Price = 129.99m,
            Name = "Wireless Headphones"
        });
    }

    [Test]
    public void Test5()
    {
        var listOfItems = order.Items.Where(x => x.Price > 50).ToList();
        foreach (var item in listOfItems)
        {
            TestContext.WriteLine($"{item.ProductId} | {item.Quantity} | {item.Price}");
        }
        listOfItems.Should().NotBeEmpty();
    }
}