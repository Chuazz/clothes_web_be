using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cosmetics.Types;

public class ShopType
{
    [BsonElement("_id")]
    public string? id { get; set; }

    [BsonElement("name")]
    public string? name { get; set; }

    [BsonElement("image")]
    public string? image { get; set; }

    [BsonElement("location_code")]
    public string? location_code { get; set; }

    [BsonElement("location")]
    public string? location { get; set; }
}