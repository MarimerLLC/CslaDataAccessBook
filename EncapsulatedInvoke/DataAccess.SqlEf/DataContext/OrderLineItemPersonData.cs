using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.SqlEf.DataContext
{
  [Table("OrderLineItemPerson")]
  public class OrderLineItemPersonData
  {
    public int LineItemId { get; set; }
    public int PersonId { get; set; }
  }
}
