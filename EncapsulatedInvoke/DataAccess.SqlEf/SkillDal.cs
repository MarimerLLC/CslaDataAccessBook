using System;
using System.Linq;
using Csla.Data;
using DataAccess.SqlEf.DataContext;

namespace DataAccess.SqlEf
{
  public class SkillDal : ISkillDal
  {
    private DatabaseContext dataContext;

    public SkillDal(DatabaseContext context)
    {
      dataContext = context;
    }

    public SafeDataReader Fetch()
    {
      var data = from r in dataContext.Skills
                 select r;
      return new SafeDataReader(new ListDataReader<SkillData>(data));
    }

    public SafeDataReader Fetch(int id)
    {
      var data = from r in dataContext.Skills
                 where r.Id == id
                 select r;
      return new SafeDataReader(new ListDataReader<SkillData>(data));
    }

    public int Insert(string name)
    {
      var data = new SkillData { Name = name };
      dataContext.Skills.Add(data);
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("SkillDal.Insert");
      return data.Id;
    }

    public void Update(int id, string name)
    {
      var item = (from r in dataContext.Skills
                  where r.Id == id
                  select r).FirstOrDefault();
      if (item == null)
        throw new DataNotFoundException("Skill");
      item.Name = name;
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("SkillDal.Update");
    }

    public void Delete(int id)
    {
        var item = (from r in dataContext.Skills
                    where r.Id == id
                    select r).First();
        dataContext.Skills.Remove(item);
        var count = dataContext.SaveChanges();
        if (count == 0)
          throw new InvalidOperationException("SkillDal.Delete");
    }
  }
}
