// -----------------------------------------------------------------------
// <copyright file="ApiSession.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Api.ApiSession;

/// <summary>
/// Propiedades de la sesión de la aplicación.
/// </summary>
public abstract class ApiSession
{
    /// <summary>
    /// Obtiene un valor para obtener el claim de usuario.
    /// </summary>
    public const string UserClaimType = "1748ddfd-88d0-41d2-b295-2f2893833d5a";

    /// <summary>
    /// Obtiene o establece un valor que indica la clave de la sesión.
    /// </summary>
    public static string? Key { get; set; }

    /// <summary>
    /// Obtiene o establece un valor que indica si es ambiente de desarrollo.
    /// </summary>
    public static bool IsDevelopment { get; set; } = true;

    /// <summary>
    /// Obtiene o establece un valor que indica si existe un usuario autenticado.
    /// </summary>
    public static bool IsAutenticated { get; set; } = false;

    /// <summary>
    /// Obtiene o establece un valor que indica si la acplicación esta habilitado para inquilinos.
    /// </summary>
    public static bool IsMultitenancy { get; set; } = false;

    /// <summary>
    /// Obtiene o establece el usuario.
    /// </summary>
    public static UserSession? User { get; set; }

    /// <summary>
    /// Obtiene o establece el inquilino.
    /// </summary>
    public static TenantSession? Tenant { get; set; }

    /// <summary>
    /// Reinicia los valores de sesion.
    /// </summary>
    public static void Clear()
    {
        Key = null;
        IsDevelopment = false;
        IsAutenticated = false;
        IsMultitenancy = false;
        User = null;
        Tenant = null;
    }
}
