using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Generator.SQLDriver
{
    public class MySqlDriver : ISQLDriver
    {
        private MySqlConnection cnn;
        private MySqlCommand cmd;
        private MySqlDataReader reader;

        public MySqlDriver()
        {
        }

        public void Close()
        {
            cnn.Close();
        }

        public bool ExecuteNonQuery(string sqlQuery)
        {
            cmd = new MySqlCommand(sqlQuery, cnn);
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void ExecuteReader(string sqlQuery)
        {
            cmd = new MySqlCommand(sqlQuery, cnn);
            reader = cmd.ExecuteReader();
        }

        public void ExecuteReader(string sqlQuery, IDictionary<string, object> parameters)
        {
            cmd = new MySqlCommand(sqlQuery, cnn);
            foreach (string key in parameters.Keys)
            {
                var parameter = cmd.CreateParameter();
                parameter.ParameterName = key;
                parameter.Value = parameters[key];
                cmd.Parameters.Add(parameter);
            }

            reader = cmd.ExecuteReader();
        }

        public object[] GetReaderValue()
        {
            object[] res = new object[reader.FieldCount];
            for (int i = 0; i < reader.FieldCount; i++)
                res[i] = reader[i];
            return res;
        }

        public List<List<object>> GetAllLinesValues()
        {
            List<List<object>> res = new List<List<object>>();
            while (Read())
            {
                List<object> line = new List<object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    line.Add(reader[i]);
                }
                res.Add(line);
            }
            reader.Close();
            return res;
        }

        public bool Open(string ConnectionString)
        {
            cnn = new MySqlConnection(ConnectionString);
            try
            {
                cnn.Open();
                Console.WriteLine("Connected DB");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool Read()
        {
            bool tmp = reader.Read();
            if (!tmp) reader.Close();
            return tmp;
        }
    }
}
