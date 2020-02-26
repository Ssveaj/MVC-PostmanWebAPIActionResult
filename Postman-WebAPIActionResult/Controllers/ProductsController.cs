using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Postman_WebAPIActionResult.Models;

namespace Postman_WebAPIActionResult.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>()
        {
            new Product
            {
                Id = 01,
                Name = "PC",
                Description ="Ink windows etc",
                Price = 5999
            },
            new Product
            {
                Id = 02,
                Name = "Laptop",
                Description = "ink visual studio etc",
                Price = 3450
            },
            new Product
            {
               Id = 03,
               Name = "HTC",
               Description = "Ink Camera etc",
               Price = 1350
            }
        };
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return products;
        }
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = products.Find(p => p.Id == id);
            
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
        // ADD NEW PRODUCT
        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            if (products.Exists(p => p.Id == product.Id))
            {
                return Conflict();
            }
            products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, products);
        }
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Product>> Delete(int id)
        {
            var product = products.Where(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            products = products.Except(product).ToList();
            
            return products;
        }
        //UPDATE PRODUCT
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<Product>> Put(int id, [FromBody] Product product)
        {
            var existingProduct = products.Where(p => p.Id == id);
            products = products.Except(existingProduct).ToList();

            products.Add(product);

            return products;
        }

    }
}

/* Make tests in postman.com */