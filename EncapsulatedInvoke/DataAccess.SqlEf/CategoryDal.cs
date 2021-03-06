﻿using System;
using System.Linq;
using Csla.Data;
using DataAccess.SqlEf.DataContext;

namespace DataAccess.SqlEf
{
  public class CategoryDal : ICategoryDal
  {
    private DatabaseContext dataContext;

    public CategoryDal(DatabaseContext context)
    {
      dataContext = context;
    }

    public SafeDataReader Fetch()
    {
      var data = from r in dataContext.Categories
                 select r;
      return new SafeDataReader(new ListDataReader<CategoryData>(data));
    }

    public int Insert(string name)
    {
      var data = new CategoryData { Category = name };
      dataContext.Categories.Add(data);
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("CategoryDal.Insert");
      return data.Id;
    }

    public void Update(int id, string name)
    {
      var item = (from r in dataContext.Categories
                  where r.Id == id
                  select r).First();
      item.Category = name;
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("CategoryDal.Update");
    }

    public void Delete(int id)
    {
      var item = (from r in dataContext.Categories
                  where r.Id == id
                  select r).First();
      dataContext.Remove(item);
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("CategoryDal.Delete");
    }
  }
}
