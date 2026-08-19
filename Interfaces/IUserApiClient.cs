using AutoTestsForApplications.DTO;
using Refit;

namespace AutoTestsForApplications.Interfaces;

[Headers("x-api-key : free_user_3HpEVY8bH2spKvIokQS9nQmkm6s")]
public interface IUserApiClient
{
    [Get("/users/{Id}")]
    Task<UserResponseDTO>  GetUserAsync(int id);
    
    [Post("/users")]
    Task<CreateUserRequestDTO>  PostUserAsync ([Body] CreateUserRequestDTO user);
    
    [Put("/users/{Id}")]
    Task<CreateUserRequestDTO> PutUserAsync(int id, [Body] CreateUserRequestDTO user);
    
    [Delete("/users/{Id}")]
    Task<ApiResponse<string>> DeleteUserAsync(int id);
}