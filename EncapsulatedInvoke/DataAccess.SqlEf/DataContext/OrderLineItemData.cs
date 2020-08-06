using System;

namespace DataAccess.SqlEf.DataContext
{
  public class OrderLineItemData
  {
    public int Id { get; set; }
    public int OrderId { get; set; }
    public DateTime? ShipDate { get; set; }
  }
}
