using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Mock
{
  public class OrderLineItemPersonDal : IOrderLineItemPersonDal
  {
    public Csla.Data.SafeDataReader Fetch(int lineItemId)
    {
      var result = from r in MockDb.MockDb.OrderLinePersons
                   where r.LineItemId == lineItemId
                   select r;
      return new Csla.Data.SafeDataReader(new ListDataReader<MockDb.OrderLineItemPersonData>(result));
    }

    public void Insert(int lineItemId, int personId)
    {
      var item = new MockDb.OrderLineItemPersonData { LineItemId = lineItemId, PersonId = personId };
      MockDb.MockDb.OrderLinePersons.Add(item);
    }

    public void DeleteAllForLineItem(int lineItemId)
    {
      var items = from r in MockDb.MockDb.OrderLinePersons
                  where r.LineItemId == lineItemId
                  select r;
      foreach (var item in items)
        MockDb.MockDb.OrderLinePersons.Remove(item);
    }

    public void Delete(int lineItemId, int personId)
    {
      var items = from r in MockDb.MockDb.OrderLinePersons
                  where r.LineItemId == lineItemId &&
                        r.PersonId == personId
                  select r;
      foreach (var item in items)
        MockDb.MockDb.OrderLinePersons.Remove(item);
    }
  }
}
