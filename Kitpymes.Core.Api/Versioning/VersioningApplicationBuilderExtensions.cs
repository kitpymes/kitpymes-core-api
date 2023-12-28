// -----------------------------------------------------------------------
// <copyright file="VersioningApplicationBuilderExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Api.Versioning;

using Microsoft.AspNetCore.Builder;

/*
    Clase de extensión VersioningApplicationBuilderExtensions
    Contiene las extensiones para la configuración del versionado
*/

/// <summary>
/// Clase de extensión <c>VersioningApplicationBuilderExtensions</c>.
/// Contiene las extensiones para la configuración del versionado.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las extensiones para la configuración del versionado.</para>
/// </remarks>
public static class VersioningApplicationBuilderExtensions
{
    /// <summary>
    /// Carga el middlware para el control de versiones.
    /// <para>
    /// La configuración del middleware de control de versiones de la API no es necesaria a menos que desee controlar explícitamente cuándo se agrega el middleware en la canalización.
    /// El control de versiones de API configura automáticamente el middleware requerido, que suele ser lo suficientemente temprano para el middleware dependiente.
    /// Además de invocar Microsoft.AspNetCore.Builder.IApplicationBuilderExtensions.UseApiVersioning(Microsoft.AspNetCore.Builder.IApplicationBuilder),
    /// también debe establecer Microsoft.AspNetCore.Mvc.Versioning.ApiVersioningOptions.RegisterMiddleware en false.
    /// </para>
    /// </summary>
    /// <param name="application">Define una clase que proporciona los mecanismos para configurar la solicitud de una aplicación.</param>
    /// <param name="enabled">Si se habilita.</param>
    /// <returns>IApplicationBuilder.</returns>
    public static IApplicationBuilder LoadApiVersioning(this IApplicationBuilder application, bool enabled = false)
    {
        if (enabled)
        {
            application.UseApiVersioning();
        }

        return application;
    }
}
