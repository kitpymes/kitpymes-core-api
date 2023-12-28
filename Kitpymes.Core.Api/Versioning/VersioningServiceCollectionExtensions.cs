// -----------------------------------------------------------------------
// <copyright file="VersioningServiceCollectionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Api.Versioning;

using System;
using Kitpymes.Core.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Kitpymes.Core.Api.Versioning.Settings;

/*
    Clase de extensión VersioningServiceCollectionExtensions
    Contiene las extensiones para los servicios del versionado de la api
*/

/// <summary>
/// Clase de extensión <c>VersioningServiceCollectionExtensions</c>.
/// Contiene las extensiones para los servicios del versionado de la api.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para el versionado de la api.</para>
/// </remarks>
public static class VersioningServiceCollectionExtensions
{
    /// <summary>
    /// Carga el servicio de versionado.
    /// <para><strong>- Ejemplos:</strong></para>
    /// <para>
    ///     conventions.Controller{HomeV1Controller}().HasApiVersion(new ApiVersion(1, 0, ApiVersionStatus.Alpha.ToString()));.
    /// </para>
    /// <para>
    ///     conventions.Controller{HomeV2Controller}().HasApiVersion(new ApiVersion(2, 0));.
    /// <para>
    /// </para>
    ///     conventions.Controller{HomeV3Controller}()
    ///         <br/> .HasDeprecatedApiVersion(3, 0)
    ///         <br/> .HasApiVersion(3, 1)
    ///         <br/> .HasApiVersion(4, 0)
    ///         <br/> .Action(c => c.Get3_0()).MapToApiVersion(3, 0)
    ///         <br/> .Action(c => c.Get3_1()).MapToApiVersion(3, 1)
    ///         <br/> .Action(c => c.Get4_0()).MapToApiVersion(4, 0);.
    /// </para>
    /// </summary>
    /// <param name="services">Colección de servicios.</param>
    /// <param name="configuration">Configuración del servicio de versionado.</param>
    /// <param name="apiVersionReader">Lector de la versión de la API.</param>
    /// <param name="conventions">Configuración de los endpoints de los controladores.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection LoadApiVersioning(
        this IServiceCollection services,
        IConfiguration configuration,
        IApiVersionReader? apiVersionReader = null,
        IApiVersionConventionBuilder? conventions = null)
    {
        var settings = configuration.Get<VersioningSettings>().ToThrowIfNullOrEmpty();

        if (apiVersionReader != null)
        {
            settings.ApiVersionReader = apiVersionReader;
        }

        if (conventions != null)
        {
            settings.Conventions = conventions;
        }

        return services.LoadApiVersioning(settings);
    }

    /// <summary>
    /// Carga el servicio de versionado.
    /// </summary>
    /// <param name="services">Colección de servicios.</param>
    /// <param name="options">Configuración del servicio de versionado.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection LoadApiVersioning(
         this IServiceCollection services,
         Action<VersioningOptions> options)
    => services.LoadApiVersioning(options.ToConfigureOrDefault().ApiVersioningSettings);

    /// <summary>
    /// Carga el servicio de versionado.
    /// </summary>
    /// <param name="services">Colección de servicios.</param>
    /// <param name="settings">Configuración del servicio de versionado.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection LoadApiVersioning(
        this IServiceCollection services,
        VersioningSettings settings)
    {
        settings.ToThrowIfNullOrEmpty();

        if (settings.Enabled == true)
        {
            services
              .AddApiVersioning(options =>
              {
                  options.DefaultApiVersion = new ApiVersion(settings.MajorVersion!.Value, settings.MinorVersion!.Value);

                  options.AssumeDefaultVersionWhenUnspecified = settings.AssumeDefaultVersionWhenUnspecified!.Value;

                  options.ApiVersionReader = settings.ApiVersionReader!;

                  options.ReportApiVersions = settings.ReportApiVersions!.Value;

                  if (settings.Conventions != null)
                  {
                      options.Conventions = settings.Conventions;
                  }
              })
              .AddVersionedApiExplorer(options =>
              {
                  options.AssumeDefaultVersionWhenUnspecified = settings.AssumeDefaultVersionWhenUnspecified!.Value;

                  options.GroupNameFormat = settings.GroupNameFormat!;

                  options.SubstituteApiVersionInUrl = settings.SubstituteApiVersionInUrl!.Value;
              });
        }

        return services;
    }
}