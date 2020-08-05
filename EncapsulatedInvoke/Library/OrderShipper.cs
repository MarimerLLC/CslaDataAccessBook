using System;
using Csla;

namespace Library
{
  [Serializable]
  public class OrderShipper : CommandBase<OrderShipper>
  {
    public static readonly PropertyInfo<int> OrderIdProperty = RegisterProperty<int>(nameof(OrderId));
    public int OrderId
    {
      get { return ReadProperty(OrderIdProperty); }
      private set { LoadProperty(OrderIdProperty, value); }
    }

    public static readonly PropertyInfo<int> ShippingNumberProperty = RegisterProperty<int>(nameof(ShippingNumber));
    public int ShippingNumber
    {
      get { return ReadProperty(ShippingNumberProperty); }
      private set { LoadProperty(ShippingNumberProperty, value); }
    }

    public static OrderShipper Ship(int orderId)
    {
      var cmd = DataPortal.Create<OrderShipper>(orderId);
      return DataPortal.Execute<OrderShipper>(cmd);
    }

    [Create]
    [RunLocal]
    private void Create(int id)
    {
      OrderId = id;
    }

    [Execute]
    private void Execute([Inject] DataAccess.IOrderDal dal)
    {
      ShippingNumber = dal.ShipOrder(OrderId);
    }
  }
}
