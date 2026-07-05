using Microsoft.AspNetCore.Mvc;
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

    //GET: api/products
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Ok(_products);
    }

    //GET by id
    [HttpGet("{id}")]
    public ActionResult<Product>GetById(int id){
        var product = _products.FirstOrDefault(p => p.Id == id);
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

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        _products.Remove(product);
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
}