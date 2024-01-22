// -----------------------------------------------------------------------
// <copyright file="ApiSessionServiceCollectionExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Api.ApiSession;

using System;
using Kitpymes.Core.Api.ApiSession.Settings;
using Kitpymes.Core.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;

/*
    Clase de extensión ApiSessionServiceCollectionExtensions
    Contiene las extensiones para los servicios de la sesión del usuario
*/

/// <summary>
/// Clase de extensión <c>ApiSessionServiceCollectionExtensions</c>.
/// Contiene las extensiones para los servicios de la sesión del usuario.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para la sesión del usuario.</para>
/// </remarks>
public static class ApiSessionServiceCollectionExtensions
{
    /// <summary>
    /// Carga el servicio de sesión.
    /// </summary>
    /// <param name="services">Colección de servicios.</param>
    /// <param name="sessionOptions">Configuración del servicio de sesión.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection LoadSession(this IServiceCollection services, Action<SessionOptions> sessionOptions)
    {
        var sessionSettings = sessionOptions.ToConfigureOrDefault().SessionSettings;

        services.LoadSession(sessionSettings);

        return services;
    }

    /// <summary>
    /// Carga el servicio de sesión con la configuración del appsettings.
    /// </summary>
    /// <param name="services">Colección de servicios.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection LoadSession(this IServiceCollection services)
    {
        var sessionSettings = services.ToSettings<SessionSettings>();

        services.LoadSession(sessionSettings);

        return services;
    }

    /// <summary>
    /// Carga el servicio de sesión con la configuración del appsettings.
    /// </summary>
    /// <param name="services">Colección de servicios.</param>
    /// <param name="sessionSettings">Configuración del servicio de sesión.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection LoadSession(this IServiceCollection services, SessionSettings? sessionSettings)
    {
        if (sessionSettings?.Enabled == true)
        {
            ApiSession.IsMultitenancy = sessionSettings.IsMultitenancy == true;

            services.AddSingleton(sessionSettings);
        }

        return services;
    }
}