using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
  public class ListDataReader : IDataReader
  {
    private List<IDataReader> _data;
    private IDataReader _current;
    private bool _isClosed = false;
    private int _resultSet = -1;
    //private int _row = -1;

    public ListDataReader()
    {
      _data = new List<IDataReader>();
    }

    public ListDataReader(IDataReader list)
      : this()
    {
      AddResult(list);
    }

    public void AddResult(IDataReader list)
    {
      _data.Add(list);
      if (_resultSet == -1)
        NextResult();
    }

    public void Close()
    {
      _isClosed = true;
      _data = null;
    }

    public int Depth
    {
      get { throw new NotImplementedException(); }
    }

    public DataTable GetSchemaTable()
    {
      throw new NotImplementedException();
    }

    public bool IsClosed
    {
      get { return _isClosed; }
    }

    public bool NextResult()
    {
      _resultSet += 1;
      if (_resultSet < _data.Count)
      {
        _current = _data[_resultSet];
        return true;
      }
      else
      {
        return false;
      }
    }

    public bool Read()
    {
      return _current.Read();
    }

    public int RecordsAffected
    {
      get { return _current.RecordsAffected; }
    }

    protected virtual void Dispose(bool flag)
    {
      _data = null;
      _current = null;
      _isClosed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    public int FieldCount
    {
      get { return _current.FieldCount; }
    }

    public bool GetBoolean(int i)
    {
      return _current.GetBoolean(i);
    }

    public byte GetByte(int i)
    {
      return _current.GetByte(i);
    }

    public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
    {
      return _current.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
    }

    public char GetChar(int i)
    {
      return _current.GetChar(i);
    }

    public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
    {
      return _current.GetChars(i, fieldoffset, buffer, bufferoffset, length);
    }

    public IDataReader GetData(int i)
    {
      return _current.GetData(i);
    }

    public string GetDataTypeName(int i)
    {
      return _current.GetDataTypeName(i);
    }

    public DateTime GetDateTime(int i)
    {
      return _current.GetDateTime(i);
    }

    public decimal GetDecimal(int i)
    {
      return _current.GetDecimal(i);
    }

    public double GetDouble(int i)
    {
      return _current.GetDouble(i);
    }

    public Type GetFieldType(int i)
    {
      return _current.GetFieldType(i);
    }

    public float GetFloat(int i)
    {
      return _current.GetFloat(i);
    }

    public Guid GetGuid(int i)
    {
      return _current.GetGuid(i);
    }

    public short GetInt16(int i)
    {
      return _current.GetInt16(i);
    }

    public int GetInt32(int i)
    {
      return _current.GetInt32(i);
    }

    public long GetInt64(int i)
    {
      return _current.GetInt64(i);
    }

    public string GetName(int i)
    {
      return _current.GetName(i);
    }

    public int GetOrdinal(string name)
    {
      return _current.GetOrdinal(name);
    }

    public string GetString(int i)
    {
      return _current.GetString(i);
    }

    public object GetValue(int i)
    {
      return _current.GetValue(i);
    }

    public int GetValues(object[] values)
    {
      return _current.GetValues(values);
    }

    public bool IsDBNull(int i)
    {
      return _current.IsDBNull(i);
    }

    public object this[string name]
    {
      get { return _current[name]; }
    }

    public object this[int i]
    {
      get { return _current[i]; }
    }
  }
  
  public class ListDataReader<T> : IDataReader
  {
    private List<System.Reflection.PropertyInfo> _header;
    private List<T> _data;
    private bool _isClosed = false;
    private int _row = -1;

    public ListDataReader(IEnumerable<T> data)
    {
      _header = new List<System.Reflection.PropertyInfo>();
      var properties = typeof(T).GetProperties();
      foreach (var item in properties)
        _header.Add(item);

      _data = new List<T>(data);
    }

    #region IDataReader Members

    public void Close()
    {
      _isClosed = true;
    }

    public int Depth
    {
      get { return 1; }
    }

    public System.Data.DataTable GetSchemaTable()
    {
      throw new NotImplementedException();
    }

    public bool IsClosed
    {
      get { return _isClosed; }
    }

    public bool NextResult()
    {
      return false;
    }

    public bool Read()
    {
      _row++;
      if (_row >= _data.Count)
        return false;
      else
        return true;
    }

    public int RecordsAffected
    {
      get { return _data.Count; }
    }

    #endregion

    #region IDisposable Members
    protected virtual void Dispose(bool flag)
    {
      _data = null;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion

    #region IDataRecord Members

    public int FieldCount
    {
      get { return _header.Count; }
    }

    public bool GetBoolean(int i)
    {
      return (bool)GetValue(_row, i);
    }

    public byte GetByte(int i)
    {
      return (byte)GetValue(_row, i);
    }

    public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
    {
      throw new NotImplementedException();
    }

    public char GetChar(int i)
    {
      return (char)GetValue(_row, i);
    }

    public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
    {
      throw new NotImplementedException();
    }

    public IDataReader GetData(int i)
    {
      throw new NotImplementedException();
    }

    public string GetDataTypeName(int i)
    {
      return GetValue(_row, i).GetType().Name;
    }

    public DateTime GetDateTime(int i)
    {
      return (DateTime)GetValue(_row, i);
    }

    public decimal GetDecimal(int i)
    {
      return (decimal)GetValue(_row, i);
    }

    public double GetDouble(int i)
    {
      return (double)GetValue(_row, i);
    }

    public Type GetFieldType(int i)
    {
      return GetValue(_row, i).GetType();
    }

    public float GetFloat(int i)
    {
      return (float)GetValue(_row, i);
    }

    public Guid GetGuid(int i)
    {
      return (Guid)GetValue(_row, i);
    }

    public short GetInt16(int i)
    {
      return (Int16)GetValue(_row, i);
    }

    public int GetInt32(int i)
    {
      return (Int32)GetValue(_row, i);
    }

    public long GetInt64(int i)
    {
      return (Int64)GetValue(_row, i);
    }

    public string GetName(int i)
    {
      return (string)_header[i].Name;
    }

    public int GetOrdinal(string name)
    {
      var result = -1;
      var pos = 0;
      foreach (var item in _header)
      {
        if (name == item.Name)
        {
          result = pos;
          break;
        }
        pos++;
      }
      if (result == -1)
        throw new ArgumentException(string.Format("Column {0} not found", name));
      return result;
    }

    public string GetString(int i)
    {
      return (string)GetValue(_row, i);
    }

    public object GetValue(int i)
    {
      return GetValue(_row, i);
    }

    private object GetValue(int row, int i)
    {
      var item = _data[row];
      var prop = _header[i];
      return prop.GetValue(item, null);
    }

    public int GetValues(object[] values)
    {
      throw new NotImplementedException();
    }

    public bool IsDBNull(int i)
    {
      return GetValue(_row, i) == null;
    }

    public object this[string name]
    {
      get { return GetValue(GetOrdinal(name)); }
    }

    public object this[int i]
    {
      get { return GetValue(i); }
    }

    #endregion
  }
}
