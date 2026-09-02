using AutoTestsForApplications.DTO.DapperDTOs;

namespace AutoTestsForApplications.Interfaces.DapperInterfaces;

public interface IReviewRepository
{
    Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync();
}