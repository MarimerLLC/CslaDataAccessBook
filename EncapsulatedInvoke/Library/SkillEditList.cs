using System;
using Csla;

namespace Library
{
  [Serializable]
  public class SkillEditList : BusinessListBase<SkillEditList, SkillEdit>
  {
    public SkillEditList()
    {
      AllowNew = false;
    }

    protected override SkillEdit AddNewCore()
    {
      var creator = DataPortal.Fetch<SkillEditCreator>();
      var item = creator.Result;
      Add(item);
      return item;
    }

    [Create]
    [RunLocal]
    private void Create()
    {
    }

    [Fetch]
    private void Fetch([Inject] DataAccess.ISkillDal dal)
    {
      using (LoadListMode)
      { 
        var data = dal.Fetch();
        while (data.Read())
          Add(DataPortal.FetchChild<SkillEdit>(data));
      }
    }

    [Update]
    [Transactional(TransactionalTypes.TransactionScope)]
    private void Update()
    {
      base.Child_Update();
    }
  }
}
