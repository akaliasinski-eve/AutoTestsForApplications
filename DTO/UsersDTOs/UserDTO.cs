using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.UsersDTOs;

public record UserDTO(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("profile")] Profile Profile,
    [property: JsonPropertyName("roles")] IReadOnlyList<string> Roles
);