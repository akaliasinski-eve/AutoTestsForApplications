using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AutoTestsForApplications.DTO.UsersDTOs;

public record GeoDTO(
    [property: JsonPropertyName("lat")] double Lat,
    [property: JsonPropertyName("lng")] double Lng
);