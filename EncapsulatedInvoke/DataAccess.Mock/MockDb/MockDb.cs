using System;
using System.Collections.Generic;

namespace DataAccess.Mock.MockDb
{
  public static class MockDb
  {
    public static List<PersonData> Persons { get; private set; }
    public static List<CategoryData> Categories { get; private set; }
    public static List<SkillData> Skills { get; private set; }
    public static List<OrderData> Orders { get; private set; }
    public static List<OrderLineItemData> OrderLineItems { get; private set; }
    public static List<OrderLineItemPersonData> OrderLinePersons { get; private set; }

    static MockDb()
    {
      Categories = new List<CategoryData>
      {
        new CategoryData { Id = 0, Category = "Misc" },
        new CategoryData { Id = 1, Category = "Employee" },
        new CategoryData { Id = 2, Category = "Vendor" },
        new CategoryData { Id = 3, Category = "Customer" }
      };

      Persons = new List<PersonData> 
      {
        new PersonData { Id = 5, FirstName = "Rocky", LastName = "Lhotka" },
        new PersonData { Id = 2, FirstName = "Jerod", LastName = "Asvidian" },
        new PersonData { Id = 1, FirstName = "Marnie", LastName = "Johanessen" },
        new PersonData { Id = 3, FirstName = "Andrew", LastName = "Smith" }
      };

      Skills = new List<SkillData>
      {
        new SkillData { Id = 3, Name = "C#" },
        new SkillData { Id = 4, Name = "VB" },
        new SkillData { Id = 8, Name = "SQL" },
        new SkillData { Id = 2, Name = "Admin" },
        new SkillData { Id = 1, Name = "Testing" },
      };

      Orders = new List<OrderData>
      {
        new OrderData { Id = 1, CustomerId = 5, OrderDate = new DateTime(2011, 1, 15), OrderEditDate = new DateTime(2011, 1, 25) },
        new OrderData { Id = 3, CustomerId = 5, OrderDate = new DateTime(2011, 2, 1), OrderEditDate = new DateTime(2011, 2, 1) },
        new OrderData { Id = 4, CustomerId = 5, OrderDate = new DateTime(2011, 2, 10), OrderEditDate = new DateTime(2011, 2, 10) }
      };

      OrderLineItems = new List<OrderLineItemData>
      {
        new OrderLineItemData { Id = 1, OrderId = 1, ShipDate = new DateTime(2011, 1, 20) },
        new OrderLineItemData { Id = 2, OrderId = 1, ShipDate = new DateTime(2011, 1, 21) },
        new OrderLineItemData { Id = 3, OrderId = 1 },
        new OrderLineItemData { Id = 4, OrderId = 3 },
        new OrderLineItemData { Id = 5, OrderId = 4 }
      };

      OrderLinePersons = new List<OrderLineItemPersonData>
      {
        new OrderLineItemPersonData { LineItemId = 1, PersonId = 2 },
        new OrderLineItemPersonData { LineItemId = 1, PersonId = 1 },
        new OrderLineItemPersonData { LineItemId = 2, PersonId = 3 },
        new OrderLineItemPersonData { LineItemId = 3, PersonId = 5 }
      };
    }
  }
}
