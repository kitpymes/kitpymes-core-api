// -----------------------------------------------------------------------
// <copyright file="AuthorizationBearerFilter.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Api.Swagger.Filters;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Kitpymes.Core.Api.Swagger.Extensions;
using Kitpymes.Core.Api.Swagger.Settings;
using Kitpymes.Core.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using YamlDotNet.Serialization.Schemas;

/// <summary>
/// Filtro para la autorización.
/// </summary>
/// <remarks>
/// Inicializa una nueva instancia de la clase <see cref="AuthorizationBearerFilter"/>.
/// </remarks>
/// <param name="serviceProvider">Provee un objeto para obtener servicios.</param>
public class AuthorizationBearerFilter(IServiceProvider serviceProvider)
    : IOperationFilter
{
    private IServiceProvider? ServiceProvider => serviceProvider;

    /// <inheritdoc/>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authorizeAttributes = context.GetControllerAndActionAttributes<AuthorizeAttribute>();

        if (authorizeAttributes?.Any() == true)
        {
            var settings = (ServiceProvider?.GetService(typeof(IOptions<SwaggerSettings>)) as IOptions<SwaggerSettings>)?.Value;

            var policies = authorizeAttributes.Select(attr => attr.Policy).Distinct().ToList();

            if (!operation.ToIsNullOrEmpty() && !settings.ToIsNullOrEmpty())
            {
                operation.Security ??= new List<OpenApiSecurityRequirement>();

                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            BearerFormat = settings.Security.BearerFormat,

                            Name = settings.Security.Name,

                            Scheme = settings.Security.Scheme,

                            In = settings.Security.ParameterLocation.ToEnum(ParameterLocation.Header),

                            Type = settings.Security.SecurityType.ToEnum(SecuritySchemeType.ApiKey),

                            Description = settings?.Security.Description,

                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,

                                Id = settings.Security.Scheme,
                            },
                        },
                        policies
                    },
                });
            }
        }
    }
}
