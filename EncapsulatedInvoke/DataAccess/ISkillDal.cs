using Csla.Data;

namespace DataAccess
{
  public interface ISkillDal
  {
    SafeDataReader Fetch();
    SafeDataReader Fetch(int id);
    int Insert(string name);
    void Update(int id, string name);
    void Delete(int id);
  }
}
