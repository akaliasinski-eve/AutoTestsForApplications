using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.UsersDTOs;

public record Root(
    [property: JsonPropertyName("data")] IReadOnlyList<UserDTO> Data
);