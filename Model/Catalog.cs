using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
  public class Catalog
  {
    public Catalog()
    {
      CatalogIds = new List<int>();
    }
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int ParentId { get; set; }
    public List<int> CatalogIds { get; set; }
  }
}
