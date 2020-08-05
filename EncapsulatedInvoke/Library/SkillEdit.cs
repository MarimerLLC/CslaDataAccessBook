using System;
using Csla;
using System.ComponentModel.DataAnnotations;

namespace Library
{
  [Serializable]
  public class SkillEdit : BusinessBase<SkillEdit>
  {
    public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
    public int Id
    {
      get { return GetProperty(IdProperty); }
      private set { LoadProperty(IdProperty, value); }
    }

    public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(nameof(Name));
    [Required]
    public string Name
    {
      get { return GetProperty(NameProperty); }
      set { SetProperty(NameProperty, value); }
    }

    [CreateChild]
    private void Create()
    {
      using (BypassPropertyChecks)
        Id = -1;
      BusinessRules.CheckRules();
    }

    [FetchChild]
    private void Fetch(System.Data.IDataReader data)
    {
      using (BypassPropertyChecks)
      {
        Id = data.GetInt32(data.GetOrdinal("Id"));
        Name = data.GetString(data.GetOrdinal("Name"));
      }
    }

    [InsertChild]
    private void Insert([Inject] DataAccess.ISkillDal dal)
    {
      using (BypassPropertyChecks)
      {
        Id = dal.Insert(Name);
      }
    }

    [UpdateChild]
    private void Update([Inject] DataAccess.ISkillDal dal)
    {
      using (BypassPropertyChecks)
      {
        dal.Update(Id, Name);
      }
    }

    [DeleteSelfChild]
    private void DeleteSelf([Inject] DataAccess.ISkillDal dal)
    {
      using (BypassPropertyChecks)
        dal.Delete(Id);
    }
  }
}
