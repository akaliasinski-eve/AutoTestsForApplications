using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.OrderDTOs;

public record DeliveryDTO(
    [property: JsonPropertyName("type")]
    string Type,
    [property: JsonPropertyName("status")]
    string Status,
    [property: JsonPropertyName("estimatedDate")]
    string EstimatedDate,
    [property: JsonPropertyName("trackingNumber")]
    string TrackingNumber
    );