using System;
using Csla;

namespace Library
{
  [Serializable]
  public class PersonEditUoW : ReadOnlyBase<PersonEditUoW>
  {
    public static readonly PropertyInfo<PersonEdit> PersonEditProperty = RegisterProperty<PersonEdit>(nameof(PersonEdit));
    public PersonEdit PersonEdit
    {
      get { return GetProperty(PersonEditProperty); }
      private set { LoadProperty(PersonEditProperty, value); }
    }

    public static readonly PropertyInfo<CategoryList> CategoryListProperty = RegisterProperty<CategoryList>(nameof(CategoryList));
    public CategoryList CategoryList
    {
      get { return GetProperty(CategoryListProperty); }
      private set { LoadProperty(CategoryListProperty, value); }
    }

    [Fetch]
    private void Fetch(int personId)
    {
      PersonEdit = DataPortal.Fetch<PersonEdit>(personId);
      CategoryList = DataPortal.Fetch<CategoryList>();
    }
  }
}
