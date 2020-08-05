using System;
using Csla;

namespace Library
{
  [Serializable]
  public class CategoryList : NameValueListBase<int, string>
  {
    [Fetch]
    private void Fetch([Inject] DataAccess.ICategoryDal dal)
    {
      using (LoadListMode)
      {
        using (var data = dal.Fetch())
        {
          while (data.Read())
          {
            Add(new NameValuePair(
              data.GetInt32(data.GetOrdinal("Id")), data.GetString(data.GetOrdinal("Category"))));
          }
        }
      }
    }
  }
}
