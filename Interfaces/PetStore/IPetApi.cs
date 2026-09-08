using AutoTestsForApplications.DTO.PetModelsDTO;
using Refit;

namespace AutoTestsForApplications.Interfaces.PetStore;

public interface IPetApi
{
    [Get("/pets")]
    Task<AllPetsResponseDTO> GetAllPetsAsync();
    [Get("/pets/{id}")]
    Task<PetDTO> GetPetByIdAsync(string id);
    [Get("/pets")]
    Task<AllPetsResponseDTO> GetAllPetsByStatusAndLimitAsync([Query] string status, [Query] int limit);
}