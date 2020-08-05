using System;
using Csla;

namespace Library
{
  [Serializable]
  public class PersonsUpdater : CommandBase<PersonsUpdater>
  {
    public static readonly PropertyInfo<PersonEdit> Person1Property = RegisterProperty<PersonEdit>(nameof(Person1));
    public PersonEdit Person1
    {
      get { return ReadProperty(Person1Property); }
      private set { LoadProperty(Person1Property, value); }
    }

    public static readonly PropertyInfo<PersonEdit> Person2Property = RegisterProperty<PersonEdit>(nameof(Person2));
    public PersonEdit Person2
    {
      get { return ReadProperty(Person2Property); }
      private set { LoadProperty(Person2Property, value); }
    }

    public static PersonsUpdater Update(PersonEdit person1, PersonEdit person2)
    {
      var cmd = DataPortal.Create<PersonsUpdater>(person1, person2);
      return DataPortal.Execute(cmd);
    }

    [Create]
    [RunLocal]
    private void Create(PersonEdit person1, PersonEdit person2)
    {
      Person1 = person1;
      Person2 = person2;
    }

    [Execute]
    [Transactional(TransactionalTypes.TransactionScope)]
    private void Execute()
    {
      Person1 = Person1.Save();
      Person2 = Person2.Save();
    }
  }
}
