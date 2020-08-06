using System;
using System.Linq;
using Csla.Data;
using DataAccess.SqlEf.DataContext;

namespace DataAccess.SqlEf
{
  public class PersonDal : IPersonDal
  {
    private DatabaseContext dataContext;

    public PersonDal(DatabaseContext context)
    {
      dataContext = context;
    }

    public SafeDataReader Fetch()
    {
      var data = from r in dataContext.Persons
                 select r;
      return new SafeDataReader(new ListDataReader<PersonData>(data));
    }

    public SafeDataReader Fetch(int id)
    {
      var data = from r in dataContext.Persons
                 where r.Id == id
                 select r;
      return new SafeDataReader(new ListDataReader<PersonData>(data));
    }

    public int Insert(string firstName, string lastName)
    {
      var data = new PersonData { FirstName = firstName, LastName = lastName };
      dataContext.Persons.Add(data);
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("PersonDal.Insert");
      return data.Id;
    }

    public void Update(int id, string firstName, string lastName)
    {
      var item = (from r in dataContext.Persons
                 where r.Id == id
                 select r).FirstOrDefault();
      if (item == null)
        throw new DataNotFoundException("Person");
      item.FirstName = firstName;
      item.LastName = lastName;
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("PersonDal.Update");
    }

    public void Delete(int id)
    {
      var item = (from r in dataContext.Persons
                  where r.Id == id
                  select r).FirstOrDefault();
      if (item == null)
        throw new DataNotFoundException("Person");
      dataContext.Persons.Remove(item);
      var count = dataContext.SaveChanges();
      if (count == 0)
        throw new InvalidOperationException("PersonDal.Delete");
    }
  }
}
