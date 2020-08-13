using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.SqlEf.DataContext
{
  [Table("Skill")]
  public class SkillData
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
  }
}
