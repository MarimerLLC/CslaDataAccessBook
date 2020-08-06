using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Csla.Data;
using DataAccess.SqlEf.DataContext;

namespace DataAccess.SqlEf
{
  public class OrderLineItemDal : IOrderLineItemDal
  {
    private DatabaseContext dataContext;
    private IOrderLineItemPersonDal lineItemPersonDal;

    public OrderLineItemDal(DatabaseContext context, IOrderLineItemPersonDal dal)
    {
      dataContext = context;
      lineItemPersonDal = dal;
    }

    public SafeDataReader Fetch(int orderId)
    {
      var data = from r in dataContext.OrderLineItems
                 where r.OrderId == orderId
                 select r;
      return new SafeDataReader(new ListDataReader<OrderLineItemData>(data));
    }

    public int Insert(int orderId, DateTime? shipDate)
    {
      var data = new OrderLineItemData { OrderId = orderId };
      if (shipDate.HasValue)
        data.ShipDate = shipDate.Value;
      dataContext.OrderLineItems.Add(data);
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("OrderLineItemDal.Insert");
      return data.Id;
    }

    public void Update(int id, int orderId, DateTime? shipDate)
    {
      var item = (from r in dataContext.OrderLineItems
                  where r.Id == id
                  select r).First();
      item.OrderId = orderId;
      item.ShipDate = shipDate.Value;
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("OrderLineItemDal.Update");
    }

    public void DeleteAllForOrder(int orderId)
    {
      // make a copy of all the line item id values
      var lineItemIds = (from r in dataContext.OrderLineItems
                         where r.OrderId == orderId
                         select r.Id).ToList();

      // loop through and delete line items
      foreach (var item in lineItemIds)
        Delete(item);
    }

    public void Delete(int lineItemId)
    {
      // delete OrderLineItemPersons data
      lineItemPersonDal.DeleteAllForLineItem(lineItemId);

      var item = (from r in dataContext.OrderLineItems
                  where r.Id == lineItemId
                  select r).First();
      dataContext.OrderLineItems.Remove(item);
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("OrderLineItemDal.Delete");
    }
  }
}
