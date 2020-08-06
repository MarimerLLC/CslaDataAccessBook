using System;

namespace DataAccess.Mock.MockDb
{
  public class OrderLineItemData
  {
    public int Id { get; set; }
    public int OrderId { get; set; }
    public DateTime? ShipDate { get; set; }
  }
}
