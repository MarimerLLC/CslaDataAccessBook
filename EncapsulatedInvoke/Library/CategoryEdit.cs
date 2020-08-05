using System;
using System.ComponentModel.DataAnnotations;
using Csla;

namespace Library
{
  [Serializable]
  public class CategoryEdit : BusinessBase<CategoryEdit>
  {
    public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
    public int Id
    {
      get => GetProperty(IdProperty);
      private set => LoadProperty(IdProperty, value);
    }

    public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(nameof(Name));
    [Required]
    public string Name
    {
      get { return GetProperty(NameProperty); }
      set { SetProperty(NameProperty, value); }
    }

    [Create]
    private void Create()
    {
      using (BypassPropertyChecks)
      {
        Id = -1;
      }
      BusinessRules.CheckRules();
    }

    [Fetch]
    private void Fetch(int id, string category)
    {
      using (BypassPropertyChecks)
      {
        Id = id;
        Name = category;
      }
    }

    [Insert]
    [Transactional(TransactionalTypes.TransactionScope)]
    private void Insert([Inject] DataAccess.ICategoryDal dal)
    {
      using (BypassPropertyChecks)
      {
        Id = dal.Insert(Name);
      }
    }

    [Update]
    [Transactional(TransactionalTypes.TransactionScope)]
    private void Update([Inject] DataAccess.ICategoryDal dal)
    {
      using (BypassPropertyChecks)
      {
        dal.Update(Id, Name);
      }
    }

    [DeleteSelf]
    [Transactional(TransactionalTypes.TransactionScope)]
    private void DeleteSelf([Inject] DataAccess.ICategoryDal dal)
    {
      using (BypassPropertyChecks)
      {
        dal.Delete(Id);
      }
    }
  }
}
