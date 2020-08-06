using Csla.Data;

namespace DataAccess
{
  public interface IPersonDal
  {
    SafeDataReader Fetch();
    SafeDataReader Fetch(int id);
    int Insert(string firstName, string lastName);
    void Update(int id, string firstName, string lastName);
    void Delete(int id);
  }
}
