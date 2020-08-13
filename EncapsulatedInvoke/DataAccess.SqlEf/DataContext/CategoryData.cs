using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.SqlEf.DataContext
{
  [Table("Category")]
  public class CategoryData
  {
    public int Id { get; set; }
    public string Category { get; set; }
  }
}
