using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Mock
{
  public class PersonDal : IPersonDal
  {
    public Csla.Data.SafeDataReader Fetch()
    {
      return new Csla.Data.SafeDataReader(new ListDataReader(new ListDataReader<MockDb.PersonData>(MockDb.MockDb.Persons)));
    }

    public Csla.Data.SafeDataReader Fetch(int id)
    {
      return new Csla.Data.SafeDataReader(new ListDataReader<MockDb.PersonData>(MockDb.MockDb.Persons.Where(r => r.Id == id)));
    }

    public int Insert(string firstName, string lastName)
    {
      var newId = MockDb.MockDb.Persons.Max(r => r.Id) + 1;
      var item = new MockDb.PersonData { Id = newId, FirstName = firstName, LastName = lastName };
      MockDb.MockDb.Persons.Add(item);
      return newId;
    }

    public void Update(int id, string firstName, string lastName)
    {
      var item = MockDb.MockDb.Persons.Where(r => r.Id == id).First();
      item.FirstName = firstName;
      item.LastName = lastName;
    }

    public void Delete(int id)
    {
      var item = MockDb.MockDb.Persons.Where(r => r.Id == id).FirstOrDefault();
      if (item != null)
        MockDb.MockDb.Persons.Remove(item);
      else
        throw new DataNotFoundException("Person");
    }
  }
}
