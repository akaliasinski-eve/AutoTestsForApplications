using System.Text.Json.Serialization;

namespace AutoTestsForApplications.DTO.OrderDTOs;

public record PaymentDTO(
    [property: JsonPropertyName("method")]
    string Method,
    [property: JsonPropertyName("status")]
    string Status,
    [property: JsonPropertyName("transactionId")]
    string TransactionId
    );