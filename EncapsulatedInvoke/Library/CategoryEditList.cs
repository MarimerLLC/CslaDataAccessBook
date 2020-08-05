using System;
using Csla;

namespace Library
{
  [Serializable]
  public class CategoryEditList : DynamicListBase<CategoryEdit>
  {
    [Fetch]
    private void Fetch([Inject] DataAccess.ICategoryDal dal)
    {
      using (LoadListMode)
      {
        using (var data = dal.Fetch())
        {
          while (data.Read())
            Add(DataPortal.Fetch<CategoryEdit>(
              data.GetInt32(data.GetOrdinal("Id")), data.GetString(data.GetOrdinal("Category"))));
        }
      }
    }
  }
}
