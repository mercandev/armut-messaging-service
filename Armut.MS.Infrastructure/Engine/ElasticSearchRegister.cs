using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Sinks.Elasticsearch;
using Serilog;

namespace Armut.MS.Infrastructure.Engine;

public static class ElasticSearchRegister
{
	public static void RegisterSerilogAndElasticSearch(this WebApplicationBuilder app, IConfiguration configuration)
	{
		app.Host.UseSerilog(configureLogger: (context, configuration) =>
		{
			configuration.Enrich.FromLogContext()
			.Enrich.WithMachineName()
			.WriteTo.Console()
			.WriteTo.Elasticsearch(
				new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Url"]))
				{
					IndexFormat = $"{context.Configuration["ApplicationName"]}--logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
					AutoRegisterTemplate = true,
					NumberOfShards = 2,
					NumberOfReplicas = 1
				})
			.Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
			.ReadFrom.Configuration(context.Configuration);
		});
	}
}

