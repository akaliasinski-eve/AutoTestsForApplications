using AutoTestsForApplications.DTO.DapperDTOs;
using AutoTestsForApplications.Interfaces.DapperInterfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace AutoTestsForApplications.DapperRepository;

public class OrderRepository : IOrderRepository
{
    private readonly string connectionString;
    
    public OrderRepository(string connection)
    {
        connectionString = connection;
    }
    
    public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
    {
        using var db = new SqliteConnection(connectionString);
        var orders = await db.QueryAsync<OrderDTO>("SELECT * FROM Orders");
        return orders;
    }
    
    public async Task<OrderDTO> GetOrderByIdAsync(int id)
    {
        using var db = new SqliteConnection(connectionString);
        var order = await db.QueryFirstOrDefaultAsync<OrderDTO>("SELECT * FROM Orders where Id = @id", new { id });
        return order;
    }
}