using System;
using Armut.MS.Infrastructure.Repository;
using Armut.MS.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Armut.MS.Infrastructure.Engine;

public static class MongoDbRegister
{
    public static void RegisterMongoDB(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
    }
}
