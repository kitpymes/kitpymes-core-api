// -----------------------------------------------------------------------
// <copyright file="SpaApplicationBuilderExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Api.Spa;

using System;
using Kitpymes.Core.Api.Spa.Settings;
using Kitpymes.Core.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.AngularCli;

/*
    Clase de extensión SpaApplicationBuilderExtensions
    Contiene las extensiones para la configuración de los servicios de las applicaciones de una sola página
*/

/// <summary>
/// Clase de extensión <c>SpaApplicationBuilderExtensions</c>.
/// Contiene las extensiones para la configuración de los servicios de las applicaciones de una sola página.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para la configuración de los servicios de las applicaciones de una sola página.</para>
/// </remarks>
public static class SpaApplicationBuilderExtensions
{
    /// <summary>
    /// Carga la configuración de una aplicación de una sola página o SPA.
    /// </summary>
    /// <param name="application">Define una clase que proporciona los mecanismos para configurar la solicitud de una aplicación.</param>
    /// <param name="options">Configuración del servicio.</param>
    /// <returns>IApplicationBuilder.</returns>
    public static IApplicationBuilder LoadSpa(
       this IApplicationBuilder application,
       Action<SpaOptions> options)
    => application.LoadSpa(options.ToConfigureOrDefault().SpaSettings);

    /// <summary>
    /// Carga la configuración de una aplicación de una sola página o SPA.
    /// </summary>
    /// <param name="application">Define una clase que proporciona los mecanismos para configurar la solicitud de una aplicación.</param>
    /// <param name="settings">Configuración del servicio.</param>
    /// <returns>IApplicationBuilder.</returns>
    public static IApplicationBuilder LoadSpa(
       this IApplicationBuilder application,
       SpaSettings settings)
    {
        settings.ToThrowIfNullOrEmpty();

        if (settings.Enabled == true)
        {
            if (settings.AngularSettings?.Enabled == true)
            {
                application.UseSpa(configuration =>
                {
                    configuration.Options.SourcePath = settings.AngularSettings.SourcePath;

                    if (!string.IsNullOrWhiteSpace(settings.AngularSettings.NpmScript))
                    {
                        configuration.UseAngularCliServer(settings.AngularSettings.NpmScript);
                    }

                    if (!string.IsNullOrWhiteSpace(settings.AngularSettings.BaseUri))
                    {
                        configuration.UseProxyToSpaDevelopmentServer(settings.AngularSettings.BaseUri);
                    }
                });
            }
        }

        return application;
    }
}
