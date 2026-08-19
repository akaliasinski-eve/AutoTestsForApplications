using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.OrderDTOs;

public record ItemDTO(
    [property: JsonPropertyName("productId")]
    int ProductId,
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonPropertyName("category")]
    string Category,
    [property: JsonPropertyName("quantity")]
    int Quantity,
    [property: JsonPropertyName("price")]
    decimal Price
    );