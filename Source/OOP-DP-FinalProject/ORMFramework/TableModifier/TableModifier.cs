using System;
using System.Collections.Generic;
using System.Reflection;
using OOPDPFinalProject.ORMFramework.SQLDriver;

namespace OOPDPFinalProject.ORMFramework.TableModifier
{
    public abstract class TableModifier
    {
        protected Dictionary<string, TableSchema.Column> columns;
        protected ISQLDriver _sqlDriver;
        protected string _tableName;
        protected string _primaryKeyName;

        protected static ISqlAdapter _adapter
        {
            get
            {
                return Session.getCurSession().SQLAdapter;
            }
        }

        private Dictionary<string, string> GetFieldValue(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            Dictionary<string, string> res = new Dictionary<string, string>();

            foreach (string fieldName in columns.Keys)
            {

                PropertyInfo propertyInfo = obj.GetType().GetProperty(fieldName);
                var value = propertyInfo.GetValue(obj, null);
                if (value == null && columns[fieldName].IsRequired())
                {
                    return null;
                }
                string name = columns[fieldName].GetName();
                string val = value.ToString();

                if (columns[fieldName].GetDataType().Equals("text"))
                {
                    val = "'" + val + "'";
                }
                res.Add(name, val);
            }
            return res;
        }

        protected abstract string getSQL(Dictionary<string, string> fieldValues);

        public bool Execute(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Dictionary<string, string> fieldValues = GetFieldValue(obj);

            if (fieldValues == null)
            {
                return false;
            }

            string sql = getSQL(fieldValues);
            //Console.WriteLine(sql);
            return _sqlDriver.ExecuteNonQuery(sql);
        }

        protected TableModifier(ISQLDriver sQLDriver, Dictionary<string, TableSchema.Column> Columns, string TableName, string PrimaryKeyName)
        {
            this._tableName = TableName;
            this._primaryKeyName = PrimaryKeyName;
            this._sqlDriver = sQLDriver;
            this.columns = Columns;
        }
    }
}
