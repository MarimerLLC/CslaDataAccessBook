using System;

namespace DataAccess.SqlEf.DataContext
{
  public class OrderData
  {
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? OrderEditDate { get; set; }
  }
}