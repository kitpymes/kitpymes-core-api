using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using Kitpymes.Core.Api.Versioning;
using Kitpymes.Core.Api.Swagger;
using Tests.Api.Swagger;
using System.Reflection;
using Kitpymes.Core.Api.Swagger.Settings;
using Kitpymes.Core.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

/***  LoadApiVersioning  ***/
/***  LoadSwagger  ***/
builder.Services
    .LoadApiVersioning(options => options.WithEnabled(builder.Services.ToIsDevelopment()).WithConventions(ApiVersioningConventions.Get))
    .LoadSwagger(options => options
        .WithEnabled(builder.Services.ToIsDevelopment())
        .WithXmlComments(Assembly.GetExecutingAssembly())
        .WithLicense(new LicenseSettings
        {
            Name = "MIT",
            Path = "https://opensource.org/licenses/MIT",
        }));

var application = builder.Build();

// Configure the HTTP request pipeline.
if (application.ToIsDevelopment())
{
    //application.UseSwagger();
    //application.UseSwaggerUI();
}

/***  LoadApiVersioning  ***/
/***  LoadSwagger  ***/
application
    .LoadApiVersioning(application.ToIsDevelopment())
    .LoadSwagger(application.ToIsDevelopment());

application.MapControllers();

await application.RunAsync();
