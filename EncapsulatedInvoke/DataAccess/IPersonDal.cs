using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess
{
  public interface IPersonDal
  {
    IDataReader Fetch();
    IDataReader Fetch(int id);
    int Insert(string firstName, string lastName);
    void Update(int id, string firstName, string lastName);
    void Delete(int id);
  }
}
