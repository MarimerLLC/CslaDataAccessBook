using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Mock
{
  public class SkillDal : ISkillDal
  {
    public Csla.Data.SafeDataReader Fetch()
    {
      var data = from r in MockDb.MockDb.Skills
                 select r;
      return new Csla.Data.SafeDataReader(new ListDataReader<MockDb.SkillData>(data));
    }

    public Csla.Data.SafeDataReader Fetch(int id)
    {
      var data = from r in MockDb.MockDb.Skills
                 where r.Id == id
                 select r;
      return new Csla.Data.SafeDataReader(new ListDataReader<MockDb.SkillData>(data));
    }

    public int Insert(string name)
    {
      var newId = MockDb.MockDb.Skills.Max(r => r.Id) + 1;
      var item = new MockDb.SkillData { Id = newId, Name = name };
      MockDb.MockDb.Skills.Add(item);
      return newId;
    }

    public void Update(int id, string name)
    {
      var item = MockDb.MockDb.Skills.Where(r => r.Id == id).First();
      item.Name = name;
    }

    public void Delete(int id)
    {
      var item = MockDb.MockDb.Skills.Where(r => r.Id == id).FirstOrDefault();
      if (item != null)
        MockDb.MockDb.Skills.Remove(item);
      else
        throw new DataNotFoundException("Skill");
    }
  }
}
