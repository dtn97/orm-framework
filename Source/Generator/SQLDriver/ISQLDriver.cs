using System;
using System.Collections.Generic;

namespace Generator.SQLDriver
{
    public interface ISQLDriver
    {
        bool Open(string ConnectionString);
        void Close();
        void ExecuteReader(string sqlQuery);
        void ExecuteReader(string sqlQuery, IDictionary<string, object> parameters);
        bool ExecuteNonQuery(string sqlQuery);
        bool Read();
        object[] GetReaderValue();
        List<List<object>> GetAllLinesValues();
    }
}
