using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using Tests.Api.Session.Settings;
using Kitpymes.Core.Shared.Extensions;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Kitpymes.Core.Api.ApiSession;
using System.Net.Http;

namespace Tests.Api.Session.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionController(IConfiguration config) : ControllerBase
{
    private readonly IConfiguration _config = config;

    [HttpGet("login")]
    [AllowAnonymous]
    public IActionResult Login([FromQuery] UserModel user)
    {        
        if (AppSession.IsAutenticated)
        {
            return Ok($"El usuario ya se encuentra logueado.");
        }

        if (user.UserName != "abc" || user.Password != "123")
        {
            return Unauthorized();
        }            

        var userSession = new UserSession
        {
            Id = Guid.NewGuid().ToString(),
            Email = HttpContext.Request.ToSubdomain() + "@gmail.com",
            Role = "Admin",
            Permissions = ["View", "Create", "Edit", "Delete"],
        }.ToSerialize();

        var claims = new List<Claim> { new(AppSession.UserClaimType, userSession) };

        var token = GetToken(claims);

        return Ok(new { token });
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet("data")]
    public IActionResult GetData()
    {
        var claimsPrincipal = HttpContext.User;

        return Ok(new
        {
            AppSession = new
            {
                AppSession.UserClaimType,
                AppSession.IsAutenticated,
                AppSession.IsDevelopment,
                AppSession.IsMultitenancy,
                AppSession.Key,
                AppSession.Tenant,
                AppSession.User,
            },
            ClaimsPrincipal = new
            {
                Claims = claimsPrincipal.Claims.Select(s => new
                {
                    s.Type,
                    s.Value
                }).ToList(),

                claimsPrincipal.Identity
            },
        });
    }

    private string GetToken(IEnumerable<Claim> claims, int expiresMinutes = 5)
    {
        var validIssuer = _config.GetValue<string>("Authentication:Schemes:Bearer:ValidIssuer");
        var validAudience = _config.GetValue<string>("Authentication:Schemes:Bearer:ValidAudience");
        var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("Authentication:Schemes:Bearer:Key")!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expiresMinutes),
            Issuer = validIssuer,
            Audience = validAudience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),  SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }
}
