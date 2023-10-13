using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cosmetics.Types;

public class ProductCategoryType
{
    [BsonElement("_id")]
    public string? _id { get; set; }

    [BsonElement("name")]
    public string? name { get; set; }
}