using System;
using System.Collections.Generic;
using Generator.SQLDriver;

namespace Generator.DatabaseMapper
{
    public abstract class DatabaseMapper
    {
        protected ISQLDriver driver;

        protected virtual string GetTablesSQL()
        {
            return "";
        }

        public void Attach(ISQLDriver driver)
        {
            this.driver = driver;
        }

        public virtual List<string> GetTables()
        {
            string sql = GetTablesSQL();
            driver.ExecuteReader(sql);
            List<List<object>> lines = driver.GetAllLinesValues();
            if (lines.Count == 0)
            {
                return null;
            }
            List<string> res = new List<string>();
            foreach (List<object> line in lines)
            {
                if (((string)line[0]).Contains("innodb_"))
                {
                    continue;
                }
                res.Add((string)line[0]);
            }
            return res;
        }

        protected virtual string GetTableSchemaSQL(string tableName)
        {
            return "";
        }

        protected virtual string DataMapper(object obj)
        {
            return "";
        }

        public virtual Dictionary<string, string> GetTableSchema(string tableName)
        {
            string sql = GetTableSchemaSQL(tableName);
            driver.ExecuteReader(sql);
            List<List<object>> lines = driver.GetAllLinesValues();
            if (lines.Count == 0)
            {
                return null;
            }
            Dictionary<string, string> res = new Dictionary<string, string>();
            foreach (List<object> line in lines)
            {
                res.Add((string)line[0], DataMapper(line[1]));
            }
            return res;
        }

        public virtual List<string> GetTablePrimaryKey(string tableName)
        {
            string sql = GetPrimaryKeySQL(tableName);
            driver.ExecuteReader(sql);
            List<List<object>> lines = driver.GetAllLinesValues();
            if (lines.Count == 0)
            {
                return null;
            }
            List<string> res = new List<string>();
            foreach (List<object> line in lines)
            {
                res.Add((string)line[0]);
            }
            return res;
        }

        protected virtual string GetPrimaryKeySQL(string tableName)
        {
            return "";
        }

        public virtual Dictionary<string, string> GetTableForeignKeys(string tableName)
        {
            string sql = GetTableForeignKeysSQL(tableName);
            driver.ExecuteReader(sql);
            List<List<object>> lines = driver.GetAllLinesValues();
            if (lines.Count == 0)
            {
                return null;
            }
            Dictionary<string, string> res = new Dictionary<string, string>();
            foreach (List<object> line in lines)
            {
                res.Add((string)line[0], (string)line[1]);
            }
            return res;
        }

        protected virtual string GetTableForeignKeysSQL(string tableName)
        {
            return "";
        }

        public virtual List<string> GetTableHaveForeignKey(string tableName)
        {
            string sql = GetTableHaveForeignKeySQL(tableName);
            driver.ExecuteReader(sql);
            List<List<object>> lines = driver.GetAllLinesValues();
            if (lines.Count == 0)
            {
                return null;
            }
            List<string> res = new List<string>();
            foreach (List<object> line in lines)
            {
                if (((string)line[0]).Contains("innodb_"))
                {
                    continue;
                }
                res.Add((string)line[0]);
            }
            return res;
        }

        protected virtual string GetTableHaveForeignKeySQL(string tableName)
        {
            return "";
        }
    }
}
