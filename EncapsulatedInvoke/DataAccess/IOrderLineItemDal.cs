using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess
{
  public interface IOrderLineItemDal
  {
    IDataReader Fetch(int orderId);
    int Insert(int orderId, DateTime? shipDate);
    void Update(int id, int orderId, DateTime? shipDate);
    void DeleteAllForOrder(int orderId);
    void Delete(int lineItemId);
  }
}
