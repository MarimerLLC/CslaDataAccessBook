using System;
using Csla;

namespace Library
{
  [Serializable]
  public class OrderLinePersons : BusinessListBase<OrderLinePersons, OrderLinePerson>
  {
    [FetchChild]
    private void Fetch(int lineItemId, [Inject] DataAccess.IOrderLineItemPersonDal dal)
    {
      using (LoadListMode)
      { 
        var data = dal.Fetch(lineItemId);
        while (data.Read())
          Add(DataPortal.FetchChild<OrderLinePerson>(data));
      }
    }
  }
}
