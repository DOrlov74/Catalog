using System;
using System.Collections.Generic;

namespace Data
{
  public interface IRepository<TEntity> where TEntity : class
  {
    TEntity GetRoot();
    TEntity Get(int id);
    TEntity Add(TEntity entity);
    void Delete(int id);
    void Update(int id, TEntity entity);
  }
}
