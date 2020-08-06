using System;
using Csla.Data;

namespace DataAccess
{
  public interface IOrderLineItemDal
  {
    SafeDataReader Fetch(int orderId);
    int Insert(int orderId, DateTime? shipDate);
    void Update(int id, int orderId, DateTime? shipDate);
    void DeleteAllForOrder(int orderId);
    void Delete(int lineItemId);
  }
}
