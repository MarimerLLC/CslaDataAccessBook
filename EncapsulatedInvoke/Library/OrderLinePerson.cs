using System;
using Csla;

namespace Library
{
  [Serializable]
  public class OrderLinePerson : BusinessBase<OrderLinePerson>
  {
    public static readonly PropertyInfo<int> LineItemIdProperty = RegisterProperty<int>(nameof(LineItemId));
    public int LineItemId
    {
      get { return GetProperty(LineItemIdProperty); }
      private set { LoadProperty(LineItemIdProperty, value); }
    }

    public static readonly PropertyInfo<int> PersonIdProperty = RegisterProperty<int>(nameof(PersonId));
    public int PersonId
    {
      get { return GetProperty(PersonIdProperty); }
      private set { LoadProperty(PersonIdProperty, value); }
    }

    [CreateChild]
    private void Create(int personId)
    {
      using (BypassPropertyChecks)
      {
        LineItemId = -1;
        PersonId = personId;
      }
    }

    [FetchChild]
    private void Fetch(Csla.Data.SafeDataReader data)
    {
      using (BypassPropertyChecks)
      {
        LineItemId = data.GetInt32("LineItemId");
        PersonId = data.GetInt32("PersonId"); 
      }
    }

    [InsertChild]
    private void Insert(OrderLineItem lineItem, [Inject] DataAccess.IOrderLineItemPersonDal dal)
    {
      using (BypassPropertyChecks)
      {
        using (BypassPropertyChecks)
        {
          LineItemId = lineItem.Id;
          dal.Insert(LineItemId, PersonId);
        }
      }
    }

    [UpdateChild]
    private void Update(OrderLineItem lineItem)
    {
      // link table, nothing to update
    }

    [DeleteSelfChild]
    private void DeleteSelf(OrderLineItem lineItem, [Inject] DataAccess.IOrderLineItemPersonDal dal)
    {
      using (BypassPropertyChecks)
      {
        dal.Delete(LineItemId, PersonId);
      }
    }
  }
}
