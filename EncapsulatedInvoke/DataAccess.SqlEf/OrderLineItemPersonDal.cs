using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla.Data;
using System.Data;
using DataAccess.SqlEf.DataContext;

namespace DataAccess.SqlEf
{
  public class OrderLineItemPersonDal : IOrderLineItemPersonDal
  {
    private DatabaseContext dataContext;

    public OrderLineItemPersonDal(DatabaseContext context)
    {
      dataContext = context;
    }

    public SafeDataReader Fetch(int lineItemId)
    {
      var lineItem = (from r in dataContext.OrderLineItems
                      where r.Id == lineItemId
                      select r).First();
      var result = from r in dataContext.OrderLineItemPersons
                   where r.LineItemId == lineItem.Id
                   select new OrderLineItemPersonData { LineItemId = r.LineItemId, PersonId = r.PersonId };
      return new SafeDataReader(new ListDataReader<OrderLineItemPersonData>(result));
    }

    public void Insert(int lineItemId, int personId)
    {
      var lineItem = (from r in dataContext.OrderLineItems
                      where r.Id == lineItemId
                      select r).First();
      var person = (from r in dataContext.Persons
                    where r.Id == personId
                    select r).First();
      dataContext.OrderLineItemPersons.Add(
        new OrderLineItemPersonData { LineItemId = lineItem.Id, PersonId = person.Id});
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("OrderLineItemPersonDal.Insert");
    }

    public void DeleteAllForLineItem(int lineItemId)
    {
      var lineItem = (from r in dataContext.OrderLineItems
                      where r.Id == lineItemId
                      select r).First();
      var data = dataContext.OrderLineItemPersons.Where(r => r.LineItemId == lineItem.Id).ToList();
      foreach (var item in data)
      {
        dataContext.OrderLineItemPersons.Remove(item);
      }
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("OrderLineItemPersonDal.Delete");
    }

    public void Delete(int lineItemId, int personId)
    {
      var lineItem = (from r in dataContext.OrderLineItemPersons
                      where r.LineItemId == lineItemId && r.PersonId == personId
                      select r).First();
      dataContext.OrderLineItemPersons.Remove(lineItem);
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("OrderLineItemPersonDal.Delete");
    }
  }
}
