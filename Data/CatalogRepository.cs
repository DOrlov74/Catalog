using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Data
{
  public class CatalogRepository : IRepository<Catalog>
  {
    public CatalogRepository(CatalogContext context) {
      Context = context;
    }
    protected CatalogContext Context { get; set; }
    public Catalog Add(Catalog entity)
    {
      entity.Id = ++CatalogContext.ids;
      Catalog parent = Context.catalogs.Find(e => e.Id == entity.ParentId);
      foreach(int id in parent.CatalogIds)
      {
        Catalog entInParent = Context.catalogs.Find(e => e.Id == id);
        if(entInParent.Name == entity.Name)
        {
          return null;
        }
      }
      parent.CatalogIds.Add(entity.Id);
      Context.catalogs.Add(entity);
      return entity;
    }

    public void Delete(int id)
    {
      Catalog catalog = Context.catalogs.Find(e => e.Id == id);
      if (catalog != null)
      {
        Catalog parent = Context.catalogs.Find(e => e.Id == catalog.ParentId);
        if (catalog.CatalogIds.Count > 0)
        {
          foreach (int cid in catalog.CatalogIds)
          {
            DeleteChildren(cid);
          }
        }
        if (parent != null && parent.CatalogIds.Find(e => e == id) > 0)
        {
          parent.CatalogIds.Remove(id);
        }
        Context.catalogs.Remove(catalog);
      }
    }

    private void DeleteChildren(int id)
    {
      Catalog catalog = Context.catalogs.Find(e => e.Id == id);
      if (catalog != null)
      {
        if (catalog.CatalogIds.Count > 0)
        {
          foreach (int cid in catalog.CatalogIds)
          {
            DeleteChildren(cid);
          }
        }
        Context.catalogs.Remove(catalog);
      }
    }

    public Catalog GetRoot()
    {
      return Context.catalogs.Find(e => e.Name == "Root");
    }

    public Catalog Get(int id)
    {
      return Context.catalogs.Find(e => e.Id == id);
    }

    public void Update(int id, Catalog entity)
    {
      Catalog ent = Context.catalogs.Find(e => e.Id == id);
      Catalog parent = Context.catalogs.Find(e => e.ParentId == ent.ParentId);
      if (ent != null)
      {
        ent.CatalogIds = entity.CatalogIds;
        ent.Name = entity.Name;
      }
      if (parent != null && parent.CatalogIds.Find(e => e == id) > 0)
      {
        Catalog entInParent = Context.catalogs.Find(e => e.Id == id);
        if (entInParent != null)
        {
          entInParent.CatalogIds = entity.CatalogIds;
          entInParent.Name = entity.Name;
        }
      }
    }
  }
}
