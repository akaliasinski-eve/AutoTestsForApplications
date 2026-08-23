using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.UsersDTOs;

public record RootDTO(
    [property: JsonPropertyName("data")] IReadOnlyList<UserDTO> Data
);