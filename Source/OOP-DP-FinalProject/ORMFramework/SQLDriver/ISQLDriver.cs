﻿using System;
using System.Collections.Generic;
using OOPDPFinalProject.ORMFramework.TableSchema;

namespace OOPDPFinalProject.ORMFramework.SQLDriver
{
    public interface ISQLDriver
    {
        void Open(string ConnectionString);
        void Close();
        void ExecuteReader(string sqlQuery);
        void ExecuteReader(string sqlQuery, IDictionary<string, object> parameters);
        bool ExecuteNonQuery(string sqlQuery);
        bool Read();
        object[] GetReaderValue();
        List<List<object>> GetAllLinesValues();
    }
}
