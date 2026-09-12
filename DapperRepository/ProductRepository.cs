using AutoTestsForApplications.DTO.DapperDTOs;
using AutoTestsForApplications.Interfaces.DapperInterfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace AutoTestsForApplications.DapperRepository;

public class ProductRepository : IProductRepository
{
    private readonly string connectionString;

    public ProductRepository(string connection)
    {
        connectionString = connection;
    }

    public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
    {
        using var db = new SqliteConnection(connectionString);
        var products = await db.QueryAsync<ProductDTO>("SELECT * FROM Products");
        return products;
    }

    public async Task<ProductDTO> GetProductByIdAsync(int id)
    {
        using var db = new SqliteConnection(connectionString);
        var product = await db.QueryFirstOrDefaultAsync<ProductDTO>("SELECT * FROM Products where Id = @id", new { id });
        return product;
    }
    
    public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryId(int categoryId)
    {
        using var db = new SqliteConnection(connectionString);
        var products = await db.QueryAsync<ProductDTO>("SELECT * FROM Products where CategoryId = @categoryId", 
            new { categoryId });
        return products;
    }
}