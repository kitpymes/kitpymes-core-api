using System;
using System.Collections.Generic;

namespace Tests.Api.Swagger
{
    /// <summary>
    /// Clase de productos.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Lista de productos.
        /// </summary>
        public static List<Product> List { get; set; } =
        [
            new () { Id = 1, Name = "John", CreatedDate = DateTime.Now },
            new () { Id = 2, Name = "Juan", CreatedDate = DateTime.Now },
            new () { Id = 3, Name = "Pedro", CreatedDate = DateTime.Now}
        ];

        /// <summary>
        /// The id of the product
        /// </summary>
        /// <example>92929292929</example>
        public int Id { get; set; }

        /// <summary>
        /// The name of the product
        /// </summary>
        /// <example>Pan</example>
        public string? Name { get; set; }

        /// <summary>
        /// The date created product
        /// </summary>
        /// <example>Pan</example>
        public DateTime CreatedDate { get; set; }
    }
}
