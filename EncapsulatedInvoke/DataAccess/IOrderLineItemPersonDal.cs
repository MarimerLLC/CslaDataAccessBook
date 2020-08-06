using Csla.Data;

namespace DataAccess
{
  public interface IOrderLineItemPersonDal
  {
    SafeDataReader Fetch(int lineItemId);
    void Insert(int lineItemId, int personId);
    void DeleteAllForLineItem(int lineItemId);
    void Delete(int lineItemId, int personId);
  }
}
