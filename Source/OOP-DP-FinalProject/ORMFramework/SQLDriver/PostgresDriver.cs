using System;
using System.Collections.Generic;
using System.Reflection;
using OOPDPFinalProject.Dummy;
using OOPDPFinalProject.ORMFramework.TableSchema;

namespace OOPDPFinalProject.ORMFramework.SQLDriver
{
    public class PostgresDriver:ISQLDriver
    {
        Npgsql.NpgsqlConnection _cnn;
        Npgsql.NpgsqlCommand _cmd;
        Npgsql.NpgsqlDataReader _reader;
        public PostgresDriver()
        {
        }

        public  void Close()
        {
            _cnn.Close();
        }

        public  bool ExecuteNonQuery(string sqlQuery)
        {
            _cmd = new Npgsql.NpgsqlCommand(sqlQuery, _cnn);
            try
            {
                _cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public  void ExecuteReader(string sqlQuery)
        {
            _cmd = new Npgsql.NpgsqlCommand(sqlQuery, _cnn);
            _reader = _cmd.ExecuteReader();
        }

        public  void ExecuteReader(string sqlQuery, IDictionary<string, object> parameters)
        {
            _cmd = new Npgsql.NpgsqlCommand(sqlQuery, _cnn);
            foreach (string key in parameters.Keys)
            {
                var parameter = _cmd.CreateParameter();
                parameter.ParameterName = key;
                parameter.Value = parameters[key];
                _cmd.Parameters.Add(parameter);
            }

            _reader = _cmd.ExecuteReader();
      
        }

        public  object[] GetReaderValue()
        {
            object[] res = new object[_reader.FieldCount];
            for (int i = 0; i < _reader.FieldCount; i++)
            {
                res[i] = _reader[i];
            }
            return res;
        }

        public List<List<object>> GetAllLinesValues()
        {
            List<List<object>> res = new List<List<object>>(); 
            while (Read())
            {
                List<object> line = new List<object>();
                for (int i = 0; i < _reader.FieldCount; i++)
                {
                    line.Add(_reader[i]);
                }
                res.Add(line);
            }
            _reader.Close();
            return res;
        }

        public  void Open(string ConnectionString)
        {
            _cnn = new Npgsql.NpgsqlConnection(ConnectionString);
            _cnn.Open();
            Console.WriteLine("Connected successfully to Database!");
        }

        public  bool Read()
        {
            bool tmp = _reader.Read();
            if (!tmp) _reader.Close();
            return tmp;
        }
    }
}
