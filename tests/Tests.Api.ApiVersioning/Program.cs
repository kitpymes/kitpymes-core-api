using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Kitpymes.Core.Api.Versioning;
using Tests.Api.ApiVersioning;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//builder.Services.AddEndpointsApiExplorer();

/***  LoadApiVersioning  ***/
builder.Services.LoadApiVersioning(options => options.WithEnabled().WithConventions(ApiVersioningConventions.Get));

var application = builder.Build();

application.MapControllers();

/***  LoadApiVersioning  ***/
application.LoadApiVersioning();

await application.RunAsync();
