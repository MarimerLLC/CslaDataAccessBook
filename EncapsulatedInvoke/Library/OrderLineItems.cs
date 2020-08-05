using System;
using Csla;

namespace Library
{
  [Serializable]
  public class OrderLineItems : BusinessListBase<OrderLineItems, OrderLineItem>
  {
    [FetchChild]
    private void Fetch(int orderId, [Inject] DataAccess.IOrderLineItemDal dal)
    {
      using (LoadListMode)
      {
        var data = dal.Fetch(orderId);
        while (data.Read())
          Add(DataPortal.FetchChild<OrderLineItem>(data));
      }
    }
  }
}
