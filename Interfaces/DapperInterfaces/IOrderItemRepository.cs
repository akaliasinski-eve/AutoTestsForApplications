using AutoTestsForApplications.DTO.DapperDTOs;

namespace AutoTestsForApplications.Interfaces.DapperInterfaces;

public interface IOrderItemRepository
{
    Task<IEnumerable<OrderItemDTO>> GetAllOrderItemsAsync();
    Task<IEnumerable<OrderItemDTO>> GetOrderItemsByOrderIdAsync(int orderId);
    Task<IEnumerable<OrderItemDTO>> GetOrderItemsByProductIdAsync(int productId);
}