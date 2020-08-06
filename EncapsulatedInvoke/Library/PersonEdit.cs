using System;
using System.ComponentModel.DataAnnotations;
using Csla;

namespace Library
{
  [Serializable]
  public class PersonEdit : BusinessBase<PersonEdit>
  {
    public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
    public int Id
    {
      get { return GetProperty(IdProperty); }
      private set { LoadProperty(IdProperty, value); }
    }

    public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(nameof(FirstName));
    [Display(Name = "First name")]
    public string FirstName
    {
      get { return GetProperty(FirstNameProperty); }
      set { SetProperty(FirstNameProperty, value); }
    }

    public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(nameof(LastName));
    [Display(Name = "Last name")]
    public string LastName
    {
      get { return GetProperty(LastNameProperty); }
      set { SetProperty(LastNameProperty, value); }
    }

    [Create]
    [RunLocal]
    private void Create()
    {
      using (BypassPropertyChecks)
      {
        Id = -1;
      }
      BusinessRules.CheckRules();
    }

    [Fetch]
    private void Fetch(int id, [Inject] DataAccess.IPersonDal dal)
    {
      using (var data = new Csla.Data.SafeDataReader(dal.Fetch(id)))
      {
        data.Read();
        using (BypassPropertyChecks)
        {
          Id = data.GetInt32("Id");
          FirstName = data.GetString("FirstName");
          LastName = data.GetString("LastName");
        }
      }
      BusinessRules.CheckRules();
    }

    [Insert]
    [Transactional(TransactionalTypes.TransactionScope)]
    private void Insert([Inject] DataAccess.IPersonDal dal)
    {
      using (BypassPropertyChecks)
      {
        Id = dal.Insert(FirstName, LastName);
      }
    }

    [Update]
    [Transactional(TransactionalTypes.TransactionScope)]
    private void Update([Inject] DataAccess.IPersonDal dal)
    {
      using (BypassPropertyChecks)
      {
        dal.Update(Id, FirstName, LastName);
      }
    }

    [DeleteSelf]
    [Transactional(TransactionalTypes.TransactionScope)]
    private void DeleteSelf([Inject] DataAccess.IPersonDal dal)
    {
      using (BypassPropertyChecks)
      {
        Delete(Id, dal);
      }
    }

    [Delete]
    [Transactional(TransactionalTypes.TransactionScope)]
    private void Delete(int id, [Inject] DataAccess.IPersonDal dal)
    {
      dal.Delete(id);
    }

    protected override void DataPortal_OnDataPortalInvoke(DataPortalEventArgs e)
    {
      // implement your behavior here
      base.DataPortal_OnDataPortalInvoke(e);
    }

    protected override void DataPortal_OnDataPortalInvokeComplete(DataPortalEventArgs e)
    {
      // implement your behavior here
      base.DataPortal_OnDataPortalInvokeComplete(e);
    }

    protected override void Child_OnDataPortalException(DataPortalEventArgs e, Exception ex)
    {
      // implement your behavior here
      base.Child_OnDataPortalException(e, ex);
    }
  }
}
