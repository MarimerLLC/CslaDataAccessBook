using System;
using Csla;

namespace Library
{
  [Serializable]
  public class PersonInfo : ReadOnlyBase<PersonInfo>
  {
    public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
    public int Id
    {
      get { return GetProperty(IdProperty); }
      private set { LoadProperty(IdProperty, value); }
    }

    public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(nameof(Name));
    public string Name
    {
      get { return GetProperty(NameProperty); }
      private set { LoadProperty(NameProperty, value); }
    }

    [FetchChild]
    private void Fetch(System.Data.IDataReader data)
    {
      Id = data.GetInt32(data.GetOrdinal("Id"));
      Name = string.Format("{0} {1}", 
        data.GetString(data.GetOrdinal("FirstName")),
        data.GetString(data.GetOrdinal("LastName")));
    }
  }
}
