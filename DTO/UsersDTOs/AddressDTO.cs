using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AutoTestsForApplications.DTO.UsersDTOs;

public record Address(
    [property: JsonPropertyName("street")] string Street,
    [property: JsonPropertyName("city")] string City,
    [property: JsonPropertyName("geo")] GeoDTO Geo
);