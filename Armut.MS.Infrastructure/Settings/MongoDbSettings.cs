using System;
namespace Armut.MS.Infrastructure.Settings;

public class MongoDbSettings : IMongoDbSettings
{
    public string DatabaseName {get; set;}
    public string ConnectionString { get; set; }
}