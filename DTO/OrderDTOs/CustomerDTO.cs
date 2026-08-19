using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.OrderDTOs;

public record CustomerDTO(
    [property: JsonPropertyName("id")]
    int Id,
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonPropertyName("email")]
    string Email,
    [property: JsonPropertyName("phone")]
    string Phone,
    [property: JsonPropertyName("address")]
    AddressDTO Address
    );