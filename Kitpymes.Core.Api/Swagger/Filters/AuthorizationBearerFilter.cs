// -----------------------------------------------------------------------
// <copyright file="AuthorizationBearerFilter.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Api.Swagger.Filters;

using System.Collections.Generic;
using System.Linq;
using Kitpymes.Core.Api.Swagger.Extensions;
using Kitpymes.Core.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

/// <summary>
/// Filtro para la autorización.
/// </summary>
public class AuthorizationBearerFilter : IOperationFilter
{
    /// <inheritdoc/>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authorizeAttributes = context.GetControllerAndActionAttributes<AuthorizeAttribute>();

        if (authorizeAttributes?.Any() == true)
        {
            var policies = authorizeAttributes.Select(attr => attr.Policy).Distinct().ToList();

            if (!operation.ToIsNullOrEmpty())
            {
                operation.Security ??= new List<OpenApiSecurityRequirement>();

                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "JWT Authentication",

                            Scheme = "Bearer",

                            BearerFormat = "JWT",

                            In = ParameterLocation.Header,

                            Type = SecuritySchemeType.Http,

                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,

                                Id = "Bearer",
                            },
                        },
                        policies
                    },
                });
            }
        }
    }
}
