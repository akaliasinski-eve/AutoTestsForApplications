using AutoTestsForApplications.DTO.DapperDTOs;

namespace AutoTestsForApplications.Interfaces.DapperInterfaces;

public interface IAddressRepository
{
    Task<IEnumerable<AddressDTO>> GetAllAddressesAsync();
    Task<AddressDTO> GetAddressByUserId(int userId);
}