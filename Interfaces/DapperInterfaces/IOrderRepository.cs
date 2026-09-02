using AutoTestsForApplications.DTO.DapperDTOs;

namespace AutoTestsForApplications.Interfaces.DapperInterfaces;

public interface IOrderRepository
{
    Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
    Task<OrderDTO> GetOrderByIdAsync(int id);
}