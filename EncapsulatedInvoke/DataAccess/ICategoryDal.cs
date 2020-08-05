using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess
{
  public interface ICategoryDal
  {
    IDataReader Fetch();
    int Insert(string name);
    void Update(int id, string name);
    void Delete(int id);
  }
}
