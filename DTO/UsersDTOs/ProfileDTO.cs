using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.UsersDTOs;

public record Profile(
    [property: JsonPropertyName("fullName")] string FullName,
    [property: JsonPropertyName("age")] int Age,
    [property: JsonPropertyName("address")] Address Address,
    [property: JsonPropertyName("tags")] IReadOnlyList<string> Tags
);