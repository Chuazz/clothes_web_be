using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cosmetics.Types;

public class CommentType
{
    [BsonElement("user")]
    public UserType? user { get; set; }

    [BsonElement("content")]
    public string? content { get; set; }

    [BsonElement("like_total")]
    public int? like_total { get; set; }

    [BsonElement("dis_like_total")]
    public int? dis_like_total { get; set; }

    [BsonElement("created_date")]
    public DateTime? created_date { get; set; }
}