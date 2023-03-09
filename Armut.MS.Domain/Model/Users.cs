using System;
using Armut.MS.Infrastructure.Attributes;
using Armut.MS.Infrastructure.Entity;
using MongoDB.Bson.Serialization.Attributes;

namespace Armut.MS.Domain.Model;

[BsonCollection("users")]
public class Users : Document
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string[] BannedUserId { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime LastLoginDate { get; set; }
}