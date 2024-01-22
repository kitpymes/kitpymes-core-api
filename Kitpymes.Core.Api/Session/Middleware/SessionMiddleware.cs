// -----------------------------------------------------------------------
// <copyright file="SessionMiddleware.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Api.Session.Middleware;

using System;
using System.Linq;
using System.Threading.Tasks;
using Kitpymes.Core.Api.ApiSession;
using Kitpymes.Core.Api.ApiSession.Settings;
using Kitpymes.Core.Shared.Extensions;
using Microsoft.AspNetCore.Http;

/*
    Clase del middlware SessionMiddleware
    Valida y carga los datos del usuario logueado
*/

/// <summary>
/// Clase del middlware <c>SessionMiddleware</c>.
/// Valida y carga los datos del usuario logueado.
/// </summary>
/// <remarks>
/// <para>En esta clase se pueden agregar todas las validaciones y la forma en que se cargan en sesión los datos del usuario logueado.</para>
/// </remarks>
/// <remarks>
/// Inicializa una nueva instancia de la clase <see cref="SessionMiddleware"/>.
/// </remarks>
/// <param name="requestDelegate">Una función que puede procesar una solicitud HTTP.</param>
public class SessionMiddleware(RequestDelegate requestDelegate)
{
    private RequestDelegate RequestDelegate => requestDelegate;

    /// <summary>
    /// Valida y carga los datos del usuario logueado.
    /// </summary>
    /// <param name="httpContext">Encapsula toda la información específica de una solicitud HTTP.</param>
    /// <returns>Task.</returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var settings = (SessionSettings)httpContext.RequestServices.ToRequiredService(typeof(SessionSettings));

        ApiSession.IsDevelopment = httpContext.RequestServices.ToIsDevelopment();

        if (settings?.IsMultitenancy == true)
        {
            ApiSession.IsMultitenancy = true;

            var subdomain = httpContext.Request.ToSubdomain();

            if (!subdomain.ToIsNullOrEmpty() && (ApiSession.Tenant.ToIsNullOrEmpty() || ApiSession.Tenant.Subdomain.ToIsNotEqual(subdomain)))
            {
                ApiSession.Key = null;
                ApiSession.IsAutenticated = false;
                ApiSession.Tenant = settings.Tenants?.FirstOrDefault(x => x.IsActive && x.Subdomain?.ToLower() == subdomain);
            }
        }

        if (!ApiSession.IsAutenticated && (httpContext.Request.ToHeader("Authorization")?
                .Any(x => x?.StartsWith("Bearer ", StringComparison.InvariantCultureIgnoreCase) ?? false) ?? false))
        {
            ApiSession.Key = Guid.NewGuid().ToSecuencial().ToString();
            ApiSession.IsAutenticated = true;
            ApiSession.User = httpContext.User.ToGet<UserSession>(ApiSession.UserClaimType).ToThrowIfNullOrEmpty();
        }

        await RequestDelegate(httpContext).ConfigureAwait(false);
    }
}