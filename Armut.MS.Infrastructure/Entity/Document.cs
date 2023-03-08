using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Armut.MS.Infrastructure.Entity;

public abstract class Document : IDocument
{
    public ObjectId Id {get; set;}

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public bool IsActive { get; set; } = true;
}
