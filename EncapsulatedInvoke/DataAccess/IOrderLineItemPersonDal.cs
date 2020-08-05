using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess
{
  public interface IOrderLineItemPersonDal
  {
    IDataReader Fetch(int lineItemId);
    void Insert(int lineItemId, int personId);
    void DeleteAllForLineItem(int lineItemId);
    void Delete(int lineItemId, int personId);
  }
}
