using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Mock
{
  public class OrderDal : IOrderDal
  {
    private IOrderLineItemDal lineItemDal;
    public OrderDal(IOrderLineItemDal dal)
    {
      lineItemDal = dal;
    }

    public int ShipOrder(int id)
    {
      // ship order and generate shipping number
      Update(id, DateTime.Today);

      var lineItems = lineItemDal.Fetch(id);
      while (lineItems.Read())
        if (lineItems.IsDBNull(lineItems.GetOrdinal("ShipDate")))
        {
          lineItemDal.Update(lineItems.GetInt32(lineItems.GetOrdinal("Id")), lineItems.GetInt32(lineItems.GetOrdinal("OrderId")), DateTime.Today);
        }
      return 123;
    }

    public Csla.Data.SafeDataReader Fetch(int id)
    {
      var result = from r in MockDb.MockDb.Orders
                   where r.Id == id
                   select r;
      return new Csla.Data.SafeDataReader(new ListDataReader<MockDb.OrderData>(result));
    }

    public int Insert(int customerId, DateTime? orderDate, DateTime? lastDate)
    {
      var newId = MockDb.MockDb.Orders.Max(r => r.Id) + 1;
      var item = new MockDb.OrderData { Id = newId, CustomerId = customerId, OrderDate = orderDate, OrderEditDate = lastDate };
      MockDb.MockDb.Orders.Add(item);
      return newId;
    }

    public void Update(int id, DateTime? lastDate)
    {
      var item = MockDb.MockDb.Orders.Where(r => r.Id == id).First();
      item.OrderEditDate = lastDate;
    }

    public void Update(int id, int customerId, DateTime? orderDate, DateTime? lastDate)
    {
      var item = MockDb.MockDb.Orders.Where(r => r.Id == id).First();
      item.CustomerId = customerId;
      item.OrderDate = orderDate;
      item.OrderEditDate = lastDate;
    }

    public void Delete(int id)
    {
      lineItemDal.Delete(id);

      var order = MockDb.MockDb.Orders.Where(r => r.Id == id).FirstOrDefault();
      if (order != null)
        MockDb.MockDb.Orders.Remove(order);
      else
        throw new DataNotFoundException("Order");
    }
  }
}
