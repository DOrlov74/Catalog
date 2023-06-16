using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Data
{
  public class CatalogContext
  {
    public CatalogContext()
    {
      catalogs = new List<Catalog>();
      ids= 1;
      catalogs.Add(
        new Catalog { Id = 1, Name="Root"}
        );
    }
    public List<Catalog> catalogs;
    public static int ids;
  }
}
