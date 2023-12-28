using Kitpymes.Core.Api.Versioning.Settings;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;

namespace Tests.Api.Swagger
{
    /// <summary>
    /// Configuración de las versiones de los endpoints.
    /// </summary>
    public static class ApiVersioningConventions
    {
        /// <summary>
        /// Obtiene las versiones de los endponits.
        /// </summary>
        public static IApiVersionConventionBuilder Get => new ApiVersionConventionBuilder()
            .AddValuesController();

        private static ApiVersionConventionBuilder AddValuesController(this ApiVersionConventionBuilder builder)
        {
            builder.Controller<Controllers.v1.ValuesController>()
               .HasDeprecatedApiVersion(1, 0)
               .HasApiVersion(1, 0)
               .Action(c => c.Get()).MapToApiVersion(1, 0);

            builder.Controller<Controllers.v2.ValuesController>()
              .HasApiVersion(2, 0, VersioningStatus.Stable.ToString())
              .Action(c => c.Get()).MapToApiVersion(2, 0);

            builder.Controller<Controllers.v3.ValuesController>()
              .HasApiVersion(3, 0, VersioningStatus.Alpha.ToString())
              .Action(c => c.Get()).MapToApiVersion(3, 0);

            return builder;
        }
    }
}
