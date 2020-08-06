using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Mock
{
  public class CategoryDal : ICategoryDal
  {
    public Csla.Data.SafeDataReader Fetch()
    {
      return new Csla.Data.SafeDataReader(new ListDataReader<MockDb.CategoryData>(MockDb.MockDb.Categories));
    }

    public int Insert(string name)
    {
      var newId = MockDb.MockDb.Categories.Max(r => r.Id) + 1;
      var item = new MockDb.CategoryData { Id = newId, Category = name };
      MockDb.MockDb.Categories.Add(item);
      return newId;
    }

    public void Update(int id, string name)
    {
      var item = MockDb.MockDb.Categories.Where(r => r.Id == id).First();
      item.Category = name;
    }

    public void Delete(int id)
    {
      var item = MockDb.MockDb.Categories.Where(r => r.Id == id).FirstOrDefault();
      if (item != null)
        MockDb.MockDb.Categories.Remove(item);
    }
  }
}
