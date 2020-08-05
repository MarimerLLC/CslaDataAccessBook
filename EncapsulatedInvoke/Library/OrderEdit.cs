using System;
using Csla;

namespace Library
{
  [Serializable]
  public class OrderEdit : BusinessBase<OrderEdit>
  {
    public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
    public int Id
    {
      get { return GetProperty(IdProperty); }
      private set { LoadProperty(IdProperty, value); }
    }

    public static readonly PropertyInfo<int> CustomerIdProperty = RegisterProperty<int>(nameof(CustomerId));
    public int CustomerId
    {
      get { return GetProperty(CustomerIdProperty); }
      private set { LoadProperty(CustomerIdProperty, value); }
    }

    public static readonly PropertyInfo<DateTime> OrderDateProperty = RegisterProperty<DateTime>(nameof(OrderDate));
    public DateTime OrderDate
    {
      get { return GetProperty(OrderDateProperty); }
      private set { LoadProperty(OrderDateProperty, value); }
    }

    public static readonly PropertyInfo<DateTime> OrderEditProperty = RegisterProperty<DateTime>(nameof(LastEdit));
    public DateTime LastEdit
    {
      get { return GetProperty(OrderEditProperty); }
      private set { LoadProperty(OrderEditProperty, value); }
    }

    public static readonly PropertyInfo<OrderLineItems> OrderLineItemsProperty = RegisterProperty<OrderLineItems>(nameof(OrderLineItems));
    public OrderLineItems OrderLineItems
    {
      get { return GetProperty(OrderLineItemsProperty); }
      private set { LoadProperty(OrderLineItemsProperty, value); }
    }

    [Create]
    [RunLocal]
    private void Create(int customerId)
    {
      using (BypassPropertyChecks)
      {
        Id = -1;
        CustomerId = customerId;
        OrderDate = DateTime.Today;
        LastEdit = DateTime.Today;
      }
      OrderLineItems = DataPortal.CreateChild<OrderLineItems>();
      BusinessRules.CheckRules();
    }

    [Fetch]
    private void Fetch(int id, [Inject] DataAccess.IOrderDal dal)
    {
      var data = dal.Fetch(id);
      data.Read();
      using (BypassPropertyChecks)
      {
        Id = data.GetInt32(data.GetOrdinal("Id"));
        CustomerId = data.GetInt32(data.GetOrdinal("CustomerId"));
        OrderDate = data.GetDateTime(data.GetOrdinal("OrderDate"));
        LastEdit = data.GetDateTime(data.GetOrdinal("OrderEditDate"));
      }
      OrderLineItems = DataPortal.FetchChild<OrderLineItems>(id);
    }

    [Insert]
    [Transactional(TransactionalTypes.TransactionScope)]
    private void Insert([Inject] DataAccess.IOrderDal dal)
    {
      OrderDate = DateTime.Today;
      LastEdit = DateTime.Today;
      using (BypassPropertyChecks)
      {
        Id = dal.Insert(CustomerId, OrderDate, LastEdit);
      }
      //FieldManager.UpdateChildren(this);
      DataPortal.UpdateChild(OrderLineItems, this);
    }

    [Update]
    [Transactional(TransactionalTypes.TransactionScope)]
    private void Update([Inject] DataAccess.IOrderDal dal)
    {
      LastEdit = DateTime.Today;
      using (BypassPropertyChecks)
      {
        dal.Update(Id, CustomerId, OrderDate, LastEdit);
      }
      FieldManager.UpdateChildren(this);
    }

    [DeleteSelf]
    [Transactional(TransactionalTypes.TransactionScope)]
    private void DeleteSelf([Inject] DataAccess.IOrderDal dal)
    {
      using (BypassPropertyChecks)
        Delete(Id, dal);
    }

    [Delete]
    [Transactional(TransactionalTypes.TransactionScope)]
    private void Delete(int id, [Inject] DataAccess.IOrderDal dal)
    {
      dal.Delete(id);
      // cascading delete removed all data, so recreate child collection
      OrderLineItems = DataPortal.CreateChild<OrderLineItems>();
    }
  }
}
