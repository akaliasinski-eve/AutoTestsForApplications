using AutoTestsForApplications.DTO.DapperDTOs;

namespace AutoTestsForApplications.Interfaces.DapperInterfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
}