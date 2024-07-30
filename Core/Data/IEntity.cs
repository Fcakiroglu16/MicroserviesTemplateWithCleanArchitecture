using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Data;

public abstract class IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;
}