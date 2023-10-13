using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cosmetics.Types;

public class ShopLocationType
{
    [BsonElement("name")]
    public string? name { get; set; }

    [BsonElement("address")]
    public string? address { get; set; }
}