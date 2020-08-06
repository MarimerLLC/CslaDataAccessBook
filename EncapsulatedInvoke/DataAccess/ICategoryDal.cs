using Csla.Data;

namespace DataAccess
{
  public interface ICategoryDal
  {
    SafeDataReader Fetch();
    int Insert(string name);
    void Update(int id, string name);
    void Delete(int id);
  }
}
