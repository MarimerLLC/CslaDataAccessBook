using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess
{
  public interface IOrderDal
  {
    int ShipOrder(int id);
    IDataReader Fetch(int id);
    int Insert(int customerId, DateTime? orderDate, DateTime? lastDate);
    void Update(int id, int customerId, DateTime? orderDate, DateTime? lastDate);
    void Update(int id, DateTime? lastDate);
    void Delete(int id);
  }
}
