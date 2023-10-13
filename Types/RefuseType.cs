using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cosmetics.Types;

public class RefuseType
{
    [BsonElement("message")]
    public string? message { get; set; }

    [BsonElement("images")]
    public string[]? images { get; set; }
}