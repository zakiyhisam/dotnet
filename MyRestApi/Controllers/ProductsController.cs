using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRestApi.Data;
using MyRestApi.Models;

namespace MyRestApi.Controllers;

[ApiController] //Tells .net this is a REST API class, enabling auto data shape check and convert response to JSON
[Route("api/[controller]")] //Routes requests to: api/products This line sets the url pattern
public class ProductsController: ControllerBase
{
    //Mock database list
    private static readonly List<Product> _products = new(){
        new Product {Id = 1, Name= "Laptop", Price= 999.99m},
        new Product {Id = 2, Name= "Headset", Price= 39.99m},
        new Product {Id = 3, Name= "Mouse", Price= 19.99m}
    };

    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    //GET: api/products
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Ok(_products); //Ok(await _context.Products.ToListAsync());
    }
    //Example for fetch from real db
    public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
    {
        return Ok(await _context.Products.ToListAsync());
    }

    //GET by id
    [HttpGet("{id}")]
    public ActionResult<Product>GetById(int id){
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        return Ok(product);
        
    }
    public async Task<ActionResult<Product>> GetByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    //Post: add new item
    [HttpPost]
    public ActionResult<Product> Create(Product newProduct)
    {
        newProduct.Id = _products.Max(p => p.Id) + 1;
        _products.Add(newProduct);
        return CreatedAtAction(nameof(GetById), new {id = newProduct.Id}, newProduct);
    }
    public async Task<ActionResult<Product>> CreateAsync(Product newProduct)
    {
        _context.Products.Add(newProduct);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetByIdAsync), new {id = newProduct});
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        _products.Remove(product);
        return NoContent();
    }
    public async Task<ActionResult> DeleteAsync (int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    } 

    [HttpPut("{id}")]
    public ActionResult<Product> Update(int id, Product updatedProduct)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        return Ok(product);
    }
    public async Task<ActionResult<Product>> UpdateAsync(int id, Product updatedProduct)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        await _context.SaveChangesAsync();
        return Ok(product);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Product>>> SeachProduct(string name, decimal minPrice)
    {
        var query = _context.Products
            .Where(p => p.Name.Contains(name) && p.Price >= minPrice)
            .OrderByDescending(p => p.Price);
        return Ok(await query.ToListAsync());
    }
}