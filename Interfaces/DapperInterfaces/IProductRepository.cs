using AutoTestsForApplications.DTO.DapperDTOs;

namespace AutoTestsForApplications.Interfaces.DapperInterfaces;

public interface IProductRepository
{
    Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
    Task<ProductDTO> GetProductByIdAsync(int id);
}