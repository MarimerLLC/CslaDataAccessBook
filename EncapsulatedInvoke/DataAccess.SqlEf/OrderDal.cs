using System;
using System.Linq;
using Csla.Data;
using DataAccess.SqlEf.DataContext;

namespace DataAccess.SqlEf
{
  public class OrderDal : IOrderDal
  {
    private DatabaseContext dataContext;
    private IOrderLineItemDal lineItemDal;

    public OrderDal(DatabaseContext context, IOrderLineItemDal dal)
    {
      dataContext = context;
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

    public SafeDataReader Fetch(int id)
    {
      var data = from r in dataContext.Orders
                 where r.Id == id
                 select r;
      return new SafeDataReader(new ListDataReader<OrderData>(data));
    }

    public int Insert(int customerId, DateTime? orderDate, DateTime? lastDate)
    {
      var data = new OrderData { CustomerId = customerId };
      if (orderDate.HasValue)
        data.OrderDate = orderDate.Value;
      if (lastDate.HasValue)
        data.OrderEditDate = lastDate.Value;
      dataContext.Orders.Add(data);
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("OrderDal.Insert");
      return data.Id;
    }

    public void Update(int id, DateTime? lastDate)
    {
      var item = (from r in dataContext.Orders
                  where r.Id == id
                  select r).First();
      item.OrderEditDate = lastDate.Value;
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("OrderDal.Update");
    }

    public void Update(int id, int customerId, DateTime? orderDate, DateTime? lastDate)
    {
      var item = (from r in dataContext.Orders
                  where r.Id == id
                  select r).First();
      item.CustomerId = customerId;
      item.OrderDate = orderDate.Value;
      item.OrderEditDate = lastDate.Value;
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("OrderDal.Update");
    }

    public void Delete(int id)
    {
      var item = (from r in dataContext.Orders
                  where r.Id == id
                  select r).First();
      dataContext.Orders.Remove(item);
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("OrderDal.Delete");
    }
  }
}
