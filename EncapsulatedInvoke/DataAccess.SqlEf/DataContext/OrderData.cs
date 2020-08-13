using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.SqlEf.DataContext
{
  [Table("Order")]
  public class OrderData
  {
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? OrderEditDate { get; set; }
  }
}