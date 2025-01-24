using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Prueba.Models;

namespace MiWebAPI.Controllers
{
  [Route("api/products")]
  public class ProductsController : ControllerBase
  {
    private readonly TodoContext context;

    public ProductsController(TodoContext context)
    {
      this.context = context;
    }

    // Obtener todos los productos
    [HttpGet]
    public async Task<List<Product>> Get()
    {
      return await context.Products.ToListAsync();
    }

    // Obtener un producto por ID
    [HttpGet("{id:int}", Name = "GetProductById")]
    public async Task<ActionResult<Product>> Get(int id)
    {
      var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

      if (product is null)
      {
        return NotFound();
      }

      return product;
    }

    // Crear un nuevo producto
    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromBody] Product product)
    {
      context.Add(product);
      await context.SaveChangesAsync();
      return CreatedAtRoute("GetProductById", new { id = product.ProductId }, product);
    }

    // Actualizar un producto existente
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] Product product)
    {
      var existsProduct = await context.Products.AnyAsync(x => x.ProductId == id);

      if (!existsProduct)
      {
        return NotFound();
      }

      product.ProductId = id;
      context.Update(product);
      await context.SaveChangesAsync();
      return NoContent();
    }

        // Eliminar un producto por ID
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if (product is null)
            {
                return NotFound();
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return NoContent();
        }

    }
}
