using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json.Serialization;
using Kitpymes.Core.Api.ApiSession;
using Tests.Api.Session.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

var defaultScheme = builder.Configuration["Authentication:DefaultScheme"];

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = defaultScheme,
        Description = $"Enter the Bearer Authorization string as following: `{defaultScheme} Generated-JWT-Token`",

        Reference = new OpenApiReference
        {
            Id = defaultScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Scheme, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = defaultScheme,
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = defaultScheme,
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services
    .AddHttpContextAccessor()
    .AddAuthorization()
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = defaultScheme;
        options.DefaultChallengeScheme = defaultScheme;
        options.DefaultScheme = defaultScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = false,
            ValidIssuer = builder.Configuration["Authentication:Schemes:Bearer:ValidIssuer"],
            ValidAudience = builder.Configuration["Authentication:Schemes:Bearer:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Schemes:Bearer:Key"]!)),
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Append("Token-Expired", "true");
                }

                return Task.CompletedTask;
            }
        };
    });

/***  Configuración de la sesión  ***/
builder.Services.LoadSession(options => options
    .WithEnabled()
    .WithMultitenancy()
    .WithTenants(
        new TenantSession
        {
            IsActive = true,
            Id = Guid.NewGuid().ToString(),
            Key = Guid.NewGuid().ToString(),
            Name = "Master",
            Subdomain = "master"
        },
        new TenantSession
        {
            IsActive = true,
            Id = Guid.NewGuid().ToString(),
            Key = Guid.NewGuid().ToString(),
            Name = "Tenant1",
            Subdomain = "tenant_1",
        },
        new TenantSession
        {
            IsActive = true,
            Id = Guid.NewGuid().ToString(),
            Key = Guid.NewGuid().ToString(),
            Name = "Tenant2",
            Subdomain = "tenant_2",
        }));

   
var application = builder.Build();

// Configure the HTTP request pipeline.
if (application.Environment.IsDevelopment())
{
    application.UseSwagger();
    application.UseSwaggerUI();
}

application.UseHttpsRedirection();

application.UseAuthentication();
application.UseAuthorization();

application.MapControllers();

/***  Configuración de la sesión  ***/
application.LoadSession();

await application.RunAsync();

