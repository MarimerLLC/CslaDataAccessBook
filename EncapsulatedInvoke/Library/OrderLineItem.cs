using System;
using Csla;

namespace Library
{
  [Serializable]
  public class OrderLineItem : BusinessBase<OrderLineItem>
  {
    public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
    public int Id
    {
      get { return GetProperty(IdProperty); }
      private set { LoadProperty(IdProperty, value); }
    }

    public static readonly PropertyInfo<DateTime?> ShipDateProperty = RegisterProperty<DateTime?>(nameof(ShipDate));
    public DateTime? ShipDate
    {
      get { return GetProperty(ShipDateProperty); }
      private set { LoadProperty(ShipDateProperty, value); }
    }

    public static readonly PropertyInfo<OrderLinePersons> PersonsProperty = RegisterProperty<OrderLinePersons>(nameof(Persons));
    public OrderLinePersons Persons
    {
      get { return GetProperty(PersonsProperty); }
      private set { LoadProperty(PersonsProperty, value); }
    }

    [CreateChild]
    private void Create()
    {
      using (BypassPropertyChecks)
      {
        Id = -1;
        Persons = DataPortal.CreateChild<OrderLinePersons>();
      }
      BusinessRules.CheckRules();
    }

    [FetchChild]
    private void Fetch(System.Data.IDataReader data)
    {
      using (BypassPropertyChecks)
      {
        Id = data.GetInt32(data.GetOrdinal("Id"));
        var shipDateIndex = data.GetOrdinal("ShipDate");
        if (!data.IsDBNull(shipDateIndex))
          ShipDate = data.GetDateTime(shipDateIndex);
        Persons = DataPortal.FetchChild<OrderLinePersons>(Id);
      }
    }

    [InsertChild]
    private void Insert(OrderEdit order, [Inject] DataAccess.IOrderLineItemDal dal)
    {
      using (BypassPropertyChecks)
      {
        using (BypassPropertyChecks)
        {
          Id = dal.Insert(order.Id, ShipDate);
        }
        FieldManager.UpdateChildren(this);
      }
    }

    [UpdateChild]
    private void Update(OrderEdit order, [Inject] DataAccess.IOrderLineItemDal dal)
    {
      using (BypassPropertyChecks)
      {
        using (BypassPropertyChecks)
        {
          dal.Update(Id, order.Id, ShipDate);
        }
        FieldManager.UpdateChildren(this);
      }
    }

    [DeleteSelfChild]
    private void DeleteSelf(OrderEdit order, [Inject] DataAccess.IOrderLineItemDal dal)
    {
      using (BypassPropertyChecks)
      {
        FieldManager.UpdateChildren(this);
        dal.Delete(Id);
        Persons = DataPortal.CreateChild<OrderLinePersons>();
      }
    }
  }
}
