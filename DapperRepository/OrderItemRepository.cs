using AutoTestsForApplications.DTO.DapperDTOs;
using AutoTestsForApplications.Interfaces.DapperInterfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace AutoTestsForApplications.DapperRepository;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly string connectionString;
    
    public OrderItemRepository(string connection)
    {
        connectionString = connection;
    }
    
    public async Task<IEnumerable<OrderItemDTO>> GetAllOrderItemsAsync()
    {
        using var db = new SqliteConnection(connectionString);
        var orderItems = await db.QueryAsync<OrderItemDTO>("SELECT * FROM OrderItems");
        return orderItems;
    }
    
    public async Task<IEnumerable<OrderItemDTO>> GetOrderItemsByOrderIdAsync(int orderId)
    {
        using var db = new SqliteConnection(connectionString);
        var orderItems = await db.QueryAsync<OrderItemDTO>("SELECT * FROM OrderItems WHERE OrderId = @orderId", new { orderId });
        return orderItems;
    }
    
    public async Task<IEnumerable<OrderItemDTO>> GetOrderItemsByProductIdAsync(int productId)
    {
        using var db = new SqliteConnection(connectionString);
        var orderItems = await db.QueryAsync<OrderItemDTO>("SELECT * FROM OrderItems WHERE ProductId = @productId", new { productId });
        return orderItems;
    }
}