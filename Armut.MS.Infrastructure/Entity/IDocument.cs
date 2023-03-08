using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Armut.MS.Infrastructure.Entity;

public interface IDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    ObjectId Id { get; set; }

    DateTime CreatedDate { get; set;}

    bool IsActive { get; set;}
}