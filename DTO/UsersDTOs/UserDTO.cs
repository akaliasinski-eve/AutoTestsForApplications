using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.UsersDTOs;

public record UserDTO(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("profile")] ProfileDTO Profile,
    [property: JsonPropertyName("roles")] IReadOnlyList<string> Roles
);