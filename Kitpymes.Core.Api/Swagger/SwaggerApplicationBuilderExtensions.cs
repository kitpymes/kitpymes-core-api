// -----------------------------------------------------------------------
// <copyright file="SwaggerApplicationBuilderExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Api.Swagger;

using System.Linq;
using Kitpymes.Core.Api.Swagger.Settings;
using Kitpymes.Core.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

/*
    Clase de extensión SwaggerApplicationBuilderExtensions
    Contiene las extensiones para la configuración de swagger
*/

/// <summary>
/// Clase de extensión <c>SwaggerApplicationBuilderExtensions</c>.
/// Contiene las extensiones para la configuración de swagger.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para la configuración de swagger.</para>
/// </remarks>
public static class SwaggerApplicationBuilderExtensions
{
    /// <summary>
    /// Carga las solicitudes de configuración de swagger.
    /// </summary>
    /// <param name="application">Define una clase que proporciona los mecanismos para configurar la solicitud de una aplicación.</param>
    /// <param name="enabled">Si se habilita.</param>
    /// <returns>IApplicationBuilder.</returns>
    public static IApplicationBuilder LoadSwagger(this IApplicationBuilder application, bool enabled = false)
    {
        if (enabled)
        {
            var apiVersionDescriptionProvider = application.ToService<IApiVersionDescriptionProvider>();

            apiVersionDescriptionProvider.ToThrowIfNullOrEmpty();

            var swaggerSettings = application.ToService<SwaggerSettings>();

            application
                .UseSwagger(options =>
                {
                   // options.RouteTemplate = $"{swaggerSettings?.RoutePrefixWithSlash}{{documentName}}/swagger.json";
                })
                .UseSwaggerUI(options =>
                {
                    // options.RoutePrefix = swaggerSettings?.RoutePrefixWithSlash;

                    // apiVersionDescriptionProvider
                    //    .ApiVersionDescriptions
                    //    .ToList()
                    //    .ForEach(description =>
                    //    {
                    //        options.SwaggerEndpoint(
                    //            $"/{swaggerSettings?.RoutePrefixWithSlash}{description.GroupName}/swagger.json",
                    //            description.GroupName.ToUpperInvariant());
                    // });
                    apiVersionDescriptionProvider?
                        .ApiVersionDescriptions
                        .ToList()
                        .ForEach(description =>
                        {
                            options.SwaggerEndpoint(
                                url: $"/swagger/{description.GroupName}/swagger.json",
                                name: description.GroupName.ToUpperInvariant());
                        });
                });
        }

        return application;
    }
}
