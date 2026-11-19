// -----------------------------------------------------------------------
// <copyright file="SessionOptions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Api.ApiSession.Settings;

using Kitpymes.Core.Api.ApiSession;
using System.Linq;

/*
   Clase de configuración SessionOptions
   Contiene las propiedades para la configuración de la sesión
*/

/// <summary>
/// Clase de configuración <c>SessionOptions</c>.
/// Contiene las propiedades para la configuración de la sesión.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las propiedades para la configuración de la sesión.</para>
/// </remarks>
public class SessionOptions
{
    /// <summary>
    /// Obtiene la configuración de la sesión.
    /// </summary>
    public SessionSettings? SessionSettings { get; private set; }

    /// <summary>
    /// Obtiene o establece un valor que indica el servicio esta habilitado.
    /// </summary>
    /// <param name="enabled">Habilita o desabilita.</param>
    /// <returns>SessionOptions.</returns>
    public SessionOptions WithEnabled(bool enabled = true)
    {
        SessionSettings ??= new SessionSettings();

        SessionSettings.Enabled = enabled;

        return this;
    }

    /// <summary>
    /// Obtiene o establece un valor que indica si la aplicación acepta inquilinos.
    /// </summary>
    /// <param name="isMultitenancy">Si es multi tenant.</param>
    /// <returns>SessionOptions.</returns>
    public SessionOptions WithMultitenancy(bool isMultitenancy = true)
    {
        SessionSettings ??= new SessionSettings();

        SessionSettings.IsMultitenancy = isMultitenancy;

        return this;
    }

    /// <summary>
    /// Para agregar inquilinos.
    /// </summary>
    /// <param name="tenants">Nuevos inquilinos.</param>
    /// <returns>SessionOptions.</returns>
    public SessionOptions WithTenants(params TenantSession[] tenants)
    {
        SessionSettings??= new SessionSettings();

        SessionSettings.Tenants??=[];

        SessionSettings.Tenants.AddRange(tenants.ToList());

        return this;
    }
}
