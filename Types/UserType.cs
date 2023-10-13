using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cosmetics.Types;

public class UserType
{
    [BsonElement("_id")]
    public string? id { get; set; }

    [BsonElement("name")]
    public string? name { get; set; }
}