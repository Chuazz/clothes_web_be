using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cosmetics.Types;

public class PropertyType
{
    [BsonElement("name")]
    public string? name { get; set; }

    [BsonElement("value")]
    public string? value { get; set; }
}