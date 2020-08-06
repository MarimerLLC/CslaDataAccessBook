using System;
using System.Collections.Generic;

namespace DataAccess
{
  public class ArrayDataReader : System.Data.IDataReader
  {
    private List<object[,]> _data;
    private bool _isClosed = false;
    private int _resultSet = 0;
    private int _row = 0;

    public ArrayDataReader(object[,] data)
    {
      _data = new List<object[,]>();
      _data.Add(data);
    }

    public ArrayDataReader(List<object[,]> data)
    {
      _data = data;
    }

    public ArrayDataReader(object[] header, IEnumerable<object[]> data)
    {
      var table = new List<object[]> { header };
      foreach (var item in data)
        table.Add(item);
      var result = new List<object[,]> { new object[,] { { table } } };
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
      _resultSet++;
      if (_resultSet < _data.Count)
        return true;
      else
        return false;
    }

    public bool Read()
    {
      _row++;
      if (_row > _data[_resultSet].GetLength(0) - 2)
        return false;
      else
        return true;
    }

    public int RecordsAffected
    {
      get { return _data[_resultSet].GetLength(0) - 1; }
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
      get { return _data[_resultSet].GetLength(1); }
    }

    public bool GetBoolean(int i)
    {
      return (bool)_data[_resultSet][_row, i];
    }

    public byte GetByte(int i)
    {
      return (byte)_data[_resultSet][_row, i];
    }

    public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
    {
      throw new NotImplementedException();
    }

    public char GetChar(int i)
    {
      return (char)_data[_resultSet][_row, i];
    }

    public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
    {
      throw new NotImplementedException();
    }

    public System.Data.IDataReader GetData(int i)
    {
      throw new NotImplementedException();
    }

    public string GetDataTypeName(int i)
    {
      return _data[_resultSet][_row, i].GetType().Name;
    }

    public DateTime GetDateTime(int i)
    {
      return (DateTime)_data[_resultSet][_row, i];
    }

    public decimal GetDecimal(int i)
    {
      return (decimal)_data[_resultSet][_row, i];
    }

    public double GetDouble(int i)
    {
      return (double)_data[_resultSet][_row, i];
    }

    public Type GetFieldType(int i)
    {
      return _data[_resultSet][_row, i].GetType();
    }

    public float GetFloat(int i)
    {
      return (float)_data[_resultSet][_row, i];
    }

    public Guid GetGuid(int i)
    {
      return (Guid)_data[_resultSet][_row, i];
    }

    public short GetInt16(int i)
    {
      return (Int16)_data[_resultSet][_row, i];
    }

    public int GetInt32(int i)
    {
      return (Int32)_data[_resultSet][_row, i];
    }

    public long GetInt64(int i)
    {
      return (Int64)_data[_resultSet][_row, i];
    }

    public string GetName(int i)
    {
      return (string)_data[_resultSet][0, i];
    }

    public int GetOrdinal(string name)
    {
      for (int i = 0; i < _data[_resultSet].GetLength(1); i++)
      {
        if (_data[_resultSet][0, i].ToString() == name)
          return i;
      }
      return -1;
    }

    public string GetString(int i)
    {
      return (string)_data[_resultSet][_row, i];
    }

    public object GetValue(int i)
    {
      return _data[_resultSet][_row, i];
    }

    public int GetValues(object[] values)
    {
      throw new NotImplementedException();
    }

    public bool IsDBNull(int i)
    {
      return _data[_resultSet][_row, i] == null;
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
