using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.SqlEf.DataContext
{
  [Table("OrderLineItem")]
  public class OrderLineItemData
  {
    [Key]
    public int Id { get; set; }
    public int OrderId { get; set; }
    public DateTime? ShipDate { get; set; }
  }
}
