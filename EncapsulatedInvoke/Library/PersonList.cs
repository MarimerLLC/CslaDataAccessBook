using System;
using Csla;

namespace Library
{
  [Serializable]
  public class PersonList : ReadOnlyListBase<PersonList, PersonInfo>
  {
    [Fetch]
    private void Fetch([Inject] DataAccess.IPersonDal dal)
    {
      using (LoadListMode)
      {
        using (var data = dal.Fetch())
        {
          while (data.Read())
          {
            var item = DataPortal.FetchChild<PersonInfo>(data);
            Add(item);
          }
        }
      }
    }
  }
}
