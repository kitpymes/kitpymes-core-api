// -----------------------------------------------------------------------
// <copyright file="UserSession.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Api.ApiSession;

using System.Collections.Generic;

/// <summary>
/// Clase para la sesión del inquilino <c>UserSession</c>.
/// Propiedades y atributos del inquilino.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las propiedades y atributos para la sesión del usuario.</para>
/// </remarks>
public class UserSession
{
    /// <summary>
    /// Obtiene o establece el id.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Obtiene o establece el email.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Obtiene o establece el rol.
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// Obtiene o establece los permisos.
    /// </summary>
    public IEnumerable<string>? Permissions { get; set; }
}
