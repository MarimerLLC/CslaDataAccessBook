using System;

namespace DataAccess
{
  public interface IDalManager : IDisposable
  {
    T GetProvider<T>() where T: class;
  }
}
