// -----------------------------------------------------------------------
// <copyright file="TenantSession.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Api.ApiSession;

/*
    Clase para la sesión del inquilino TenantSession
    Propiedades y atributos del inquilino
*/

/// <summary>
/// Clase para la sesión del inquilino <c>TenantSession</c>.
/// Propiedades y atributos del inquilino.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las propiedades y atributos para la sesión del inquilino.</para>
/// </remarks>
public class TenantSession
{
    /// <summary>
    /// Obtiene o establece el id del inquilino.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Obtiene o establece la key del inquilino.
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre del inquilino.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre del subdominio del inquilino.
    /// </summary>
    public string? Subdomain { get; set; }

    /// <summary>
    /// Obtiene un valor que indica si el inquilino es master.
    /// </summary>
    public bool IsMaster => Name == "Master";

    /// <summary>
    /// Obtiene o establece un valor que indica si esta activo.
    /// </summary>
    public bool IsActive { get; set; }
}
