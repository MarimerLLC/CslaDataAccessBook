using System;
using System.Linq;

namespace DataAccess.Mock
{
  public class OrderLineItemDal : IOrderLineItemDal
  {
    private IOrderLineItemPersonDal lineItemPersonDal;

    public OrderLineItemDal(IOrderLineItemPersonDal dal)
    {
      lineItemPersonDal = dal;
    }

    public Csla.Data.SafeDataReader Fetch(int orderId)
    {
      var result = from r in MockDb.MockDb.OrderLineItems
                   where r.OrderId == orderId
                   select r;
      return new Csla.Data.SafeDataReader(new ListDataReader<MockDb.OrderLineItemData>(result));
    }

    public int Insert(int orderId, DateTime? shipDate)
    {
      var newId = MockDb.MockDb.OrderLineItems.Max(r => r.Id) + 1;
      var item = new MockDb.OrderLineItemData { Id = newId, OrderId = orderId, ShipDate = shipDate };
      MockDb.MockDb.OrderLineItems.Add(item);
      return newId;
    }

    public void Update(int id, int orderId, DateTime? shipDate)
    {
      var item = MockDb.MockDb.OrderLineItems.Where(r => r.Id == id).First();
      item.OrderId = orderId;
      item.ShipDate = shipDate;
    }

    public void DeleteAllForOrder(int orderId)
    {
      var lineItems = MockDb.MockDb.OrderLineItems.Where(r => r.OrderId == orderId);
      foreach (var item in lineItems)
      {
        lineItemPersonDal.DeleteAllForLineItem(item.Id);
        MockDb.MockDb.OrderLineItems.Remove(item);
      }
    }

    public void Delete(int lineItemId)
    {
      var lineItems = MockDb.MockDb.OrderLineItems.Where(r => r.Id == lineItemId);
      foreach (var item in lineItems)
      {
        // delete all associated person link data
        lineItemPersonDal.DeleteAllForLineItem(item.Id);
        // delete line item
        MockDb.MockDb.OrderLineItems.Remove(item);
      }
    }
  }
}
