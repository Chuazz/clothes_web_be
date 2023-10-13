using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cosmetics.Types;

public class OrderProductType
{
    [BsonElement("_id")]
    public string id { get; set; } = null!;

    [BsonElement("name")]
    public string? name { get; set; }

    [BsonElement("image")]
    public string? image { get; set; }

    [BsonElement("original_price")]
    public int? original_price { get; set; }

    [BsonElement("price")]
    public int? price { get; set; }

    [BsonElement("qty")]
    public int qty { get; set; }
}