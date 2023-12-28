// -----------------------------------------------------------------------
// <copyright file="StatusCodesFilter.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Api.Swagger.Filters;

using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Kitpymes.Core.Shared.Extensions;
using Kitpymes.Core.Shared;

/// <summary>
/// Filtro para los HTTP Status.
/// </summary>
public sealed class StatusCodesFilter : IOperationFilter
{
    /// <inheritdoc/>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!operation.ToIsNullOrEmpty())
        {
            operation.Responses ??=[];

            operation.Responses.Add(
                StatusCodes.Status500InternalServerError.ToStringFormat(),
                new OpenApiResponse { Description = SharedResource.MsgFriendlyUnexpectedError });

            var authorizeAttributes = context?.MethodInfo?.DeclaringType?
                .GetCustomAttributes(true)?
                    .Union(context.MethodInfo.GetCustomAttributes(true))?
                    .OfType<AuthorizeAttribute>();

            if (authorizeAttributes?.Any() == true)
            {
                var string401 = StatusCodes.Status401Unauthorized.ToStringFormat();

                if (!operation.Responses.Any(r => r.Key == string401))
                {
                    operation.Responses.Add(string401, new OpenApiResponse
                    {
                        Description = SharedResource.MsgUnauthorizedAccess,
                    });
                }
            }
        }
    }
}
