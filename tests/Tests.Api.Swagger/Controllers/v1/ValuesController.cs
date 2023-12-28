using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Tests.Api.Swagger.Controllers.v1
{
    /// <summary>
    /// Controlador de la V1 de la api.
    /// </summary>
    public class ValuesController : ApiControllerBase
    {
        /// <summary>
        ///  GET api/values
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns list of products</response>
        [HttpGet]
        [Produces("application/json")]        
        [ProducesResponseType(typeof(Product[]), StatusCodes.Status200OK)]
        public IActionResult Get() => Ok(Product.List);

        /// <summary>
        /// GET api/values/5
        /// here id is 5
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns product</response>
        /// <response code="400">Noway, just for demonstration</response>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Get(int id)
        {
            var product = Product.List.FirstOrDefault(x => x.Id == id);

            return product is null
                ? BadRequest($"El proucto con el id '{id}' no fue en contrao.")
                : Ok(product);
        }

        /// <summary>
        /// POST api/values
        /// </summary>
        /// <param name="model">[FromBody] Product </param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns product created</response>
        /// <response code="400">Noway, just for demonstration</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] AddProductModel model)
        {
            if (model is null)
            {
                return BadRequest($"El modelo de tipo '{nameof(AddProductModel)}' no puede ser nulo.");
            }

            var exists = Product.List.Any(x => x.Name == model.Name);

            if (exists)
            {
                return BadRequest($"El producto con el nombre '{model.Name}' ya existe.");
            }

            Product.List.Add(new Product
            {
                Id = Product.List.Last().Id + 1,
                Name = model.Name,
                CreatedDate = DateTime.Now
            });

            return Ok(Product.List.Last());
        }

        /// <summary>
        /// PUT api/values/5
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="model">[FromBody] Product</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns product updated</response>
        /// <response code="400">Noway, just for demonstration</response>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id, [FromBody] EditProductModel model)
        {
            if (model is null)
            {
                return BadRequest($"El modelo de tipo '{nameof(EditProductModel)}' no puede ser nulo.");
            }

            var product = Product.List.FirstOrDefault(x => x.Id == id);

            if (product is null)
            {
                return BadRequest($"El producto con el id '{id}' no ya existe.");
            }

            product.Name = model.Name;

            return Ok(product);
        }

        /// <summary>
        /// DELETE api/values/5
        /// </summary>
        /// <param name="id">int</param>
        /// <response code="200">Returns true si product sucesss deleted or false if not deleted</response>
        /// <response code="400">Noway, just for demonstration</response>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            var product = Product.List.FirstOrDefault(x => x.Id == id);

            if (product is null)
            {
                return BadRequest($"El producto con el id '{id}' no existe.");
            }

            Product.List.RemoveAll(x => x.Id == id);

            return Ok(true);
        }
    }
}
