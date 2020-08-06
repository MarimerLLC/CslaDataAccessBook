using System;
using Csla.Data;

namespace DataAccess
{
  public interface IOrderDal
  {
    int ShipOrder(int id);
    SafeDataReader Fetch(int id);
    int Insert(int customerId, DateTime? orderDate, DateTime? lastDate);
    void Update(int id, int customerId, DateTime? orderDate, DateTime? lastDate);
    void Update(int id, DateTime? lastDate);
    void Delete(int id);
  }
}
