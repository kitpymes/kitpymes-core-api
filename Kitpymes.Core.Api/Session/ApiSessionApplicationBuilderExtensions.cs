// -----------------------------------------------------------------------
// <copyright file="ApiSessionApplicationBuilderExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Api.ApiSession;

using Kitpymes.Core.Api.ApiSession.Settings;
using Kitpymes.Core.Api.Session.Middleware;
using Kitpymes.Core.Shared.Extensions;
using Microsoft.AspNetCore.Builder;

/*
    Clase de extensión ApiSessionApplicationBuilderExtensions
    Contiene las extensiones para la sesión del usuario
*/

/// <summary>
/// Clase de extensión <c>ApiSessionApplicationBuilderExtensions</c>.
/// Contiene las extensiones para la configuración de la sesión del usuario.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para la sesión del usuario.</para>
/// </remarks>
public static class ApiSessionApplicationBuilderExtensions
{
    /// <summary>
    /// Carga el middlware para la configuración del usuario logueado.
    /// </summary>
    /// <param name="application">Define una clase que proporciona los mecanismos para configurar la solicitud de una aplicación.</param>
    /// <returns>IApplicationBuilder.</returns>
    public static IApplicationBuilder LoadSession(this IApplicationBuilder application)
    {
        var settings = application.ToService<SessionSettings>();

        if (settings?.Enabled == true)
        {
            application.UseMiddleware<SessionMiddleware>();
        }

        return application;
    }
}
