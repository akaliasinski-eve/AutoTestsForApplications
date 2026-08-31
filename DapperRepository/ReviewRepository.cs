using AutoTestsForApplications.DTO.DapperDTOs;
using AutoTestsForApplications.Interfaces.DapperInterfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace AutoTestsForApplications.DapperRepository;

public class ReviewRepository : IReviewRepository
{
    private readonly string connectionString;
    
    public ReviewRepository(string connection)
    {
        connectionString = connection;
    }
    
    public async Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync()
    {
        using var db = new SqliteConnection(connectionString);
        var reviews = await db.QueryAsync<ReviewDTO>("SELECT * FROM Reviews");
        return reviews;
    }
}