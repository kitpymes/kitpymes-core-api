// -----------------------------------------------------------------------
// <copyright file="SessionSettings.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Api.ApiSession.Settings;

using Kitpymes.Core.Shared.Extensions;
using System;
using System.Collections.Generic;

/*
   Clase de configuración SessionSettings
   Contiene las propiedades para la configuración de la sesión
*/

/// <summary>
/// Clase de configuración <c>SessionSettings</c>.
/// Contiene las propiedades para la configuración de la sesión.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las propiedades para la configuración de la sesión.</para>
/// </remarks>
public class SessionSettings
{
    /// <summary>
    /// Valor por defecto que indica si esta habilitado el servicio.
    /// </summary>
    public const bool DefaultEnabled = false;

    /// <summary>
    /// Valor por defecto que indica si esta habilitado para inquilinos.
    /// </summary>
    public const bool DefaultMultitenancyEnabled = false;

    /// <summary>
    /// Valor por defecto que indica si es el ambiente de desarrollo.
    /// </summary>
    public const bool DefaultIsDevelopment = false;

    /// <summary>
    /// Valor por defecto que indica la clave de la sesión.
    /// </summary>
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA2211 // Los campos no constantes no deben ser visibles
    public static string DefaultKey = Guid.NewGuid().ToSecuencial().ToString();
#pragma warning restore CA2211 // Los campos no constantes no deben ser visibles
#pragma warning restore SA1401 // Fields should be private

    private bool _enabled = DefaultEnabled;

    private bool _multitenancyEnabled = DefaultMultitenancyEnabled;

    private string _key = DefaultKey;

    private bool _isDevelopment = DefaultIsDevelopment;

    /// <summary>
    /// Obtiene o establece un valor que indica el servicio esta habilitado.
    /// <para><strong>Default:</strong> <see cref="DefaultEnabled"/> = false.</para>
    /// </summary>
    public bool? Enabled
    {
        get => _enabled;
        set
        {
            if (value.HasValue)
            {
                _enabled = value.Value;
            }
        }
    }

    /// <summary>
    /// Obtiene o establece un valor que indica la key de la aplicación.
    /// <para><strong>Default:</strong> <see cref="DefaultKey"/> = Guid.</para>
    /// </summary>
    public string? Key
    {
        get => _key;
        set
        {
            if (!value.ToIsNullOrEmpty())
            {
                _key = value;
            }
        }
    }

    /// <summary>
    /// Obtiene o establece un valor que indica si la aplicación acepta inquilinos.
    /// <para><strong>Default:</strong> <see cref="DefaultMultitenancyEnabled"/> = false.</para>
    /// </summary>
    public bool? IsMultitenancy
    {
        get => _multitenancyEnabled;
        set
        {
            if (value.HasValue)
            {
                _multitenancyEnabled = value.Value;
            }
        }
    }

    /// <summary>
    /// Obtiene o establece un valor que indica si la aplicación acepta inquilinos.
    /// <para><strong>Default:</strong> <see cref="DefaultIsDevelopment"/> = false.</para>
    /// </summary>
    public bool? IsDevelopment
    {
        get => _isDevelopment;
        set
        {
            if (value.HasValue)
            {
                _isDevelopment = value.Value;
            }
        }
    }

    /// <summary>
    /// Obtiene o establece un valor que indica el usuario de la aplicación.
    /// </summary>
    public UserSession? User { get; set; }

    /// <summary>
    /// Obtiene o establece un valor que indica los inquilinos de la aplicación.
    /// </summary>
    public List<TenantSession>? Tenants { get; set; }
}
