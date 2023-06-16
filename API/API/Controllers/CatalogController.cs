using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Model;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CatalogController : ControllerBase
  {
    protected readonly IRepository<Catalog> _repo;
    public CatalogController(IRepository<Catalog> repo)
    {
      _repo = repo;
    }
    // GET: api/<CatalogController>
    [HttpGet]
    public string Get()
    {
      var root = _repo.GetRoot();
      return JsonSerializer.Serialize(root);
    }

    // GET api/<CatalogController>/5
    [HttpGet("{id}", Name = "GetCatalog")]
    public string Get(int id)
    {
      var catalog = _repo.Get(id);
      return JsonSerializer.Serialize(catalog);
    }

    // POST api/<CatalogController>
    [HttpPost]
    public ActionResult<Catalog> Post([FromBody] Catalog catalog)
    {
      var parent = _repo.Get(catalog.ParentId);
      if (parent == null)
      {
        return NotFound();
      }
      _repo.Add(catalog);
      return CreatedAtRoute("GetCatalog", new { id = catalog.Id.ToString() }, catalog);
    }

    // PUT api/<CatalogController>/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Catalog catalogIn)
    {
      var catalog = _repo.Get(id);
      if (catalog == null)
      {
        return NotFound();
      }
      _repo.Update(id, catalogIn);
      return NoContent();
    }

    // DELETE api/<CatalogController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var catalog = _repo.Get(id);
      if (catalog == null)
      {
        return NotFound();
      }
      _repo.Delete(catalog.Id);
      return NoContent();
    }
  }
}
