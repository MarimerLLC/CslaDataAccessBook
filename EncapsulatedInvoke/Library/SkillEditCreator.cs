using System;
using Csla;

namespace Library
{
  [Serializable]
  public class SkillEditCreator : ReadOnlyBase<SkillEditCreator>
  {
    public static readonly PropertyInfo<SkillEdit> ResultProperty =
      RegisterProperty<SkillEdit>(nameof(Result));
    public SkillEdit Result
    {
      get { return GetProperty(ResultProperty); }
      private set { LoadProperty(ResultProperty, value); }
    }

    [Fetch]
    private void Fetch()
    {
      Result = DataPortal.CreateChild<SkillEdit>();
    }
  }
}
