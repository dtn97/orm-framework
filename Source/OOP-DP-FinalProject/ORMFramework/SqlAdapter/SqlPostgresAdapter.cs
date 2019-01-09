using System;
using System.Collections.Generic;

namespace OOPDPFinalProject.ORMFramework
{
    public class SqlPostgresAdapter : SqlAdapterBase, ISqlAdapter
    {
        public SqlPostgresAdapter()
        {
        }   

        public string DeleteString(string tableName, string primaryKeyName, Dictionary<string, string> fieldValues)
        {
            string res = "delete from " + tableName;
            res += " WHERE " + primaryKeyName + " = " + fieldValues[primaryKeyName];
            return res;
        }

        public string Field(string tableName, string fieldName)
        {
            return string.Format("{0}.{1}", tableName, fieldName);
        }

        public string InsertString(string tableName, Dictionary<string, string> fieldValues)
        {
            string res = "insert into " + tableName;
            string fields = "";
            string values = "";

            foreach (KeyValuePair<string, string> fieldValue in fieldValues)
            {
                fields += (fieldValue.Key + ",");
                values += (fieldValue.Value + ",");
            }

            fields = fields.Substring(0, fields.Length - 1);
            values = values.Substring(0, values.Length - 1);

            res += ("(" + fields + ") values (" + values + ")");

            return res;
        }

        public virtual string OffSetAndLimit(int offset, int limit)
        {
            string tmp = "";
            if (offset >= 0) tmp += " OFFSET " + offset.ToString();
            if (limit >= 0) tmp += " LIMIT " + limit.ToString();
            return tmp;
        }

        public string Parameter(string parameterId)
        {
            return "@" + parameterId;
        }

        public string QueryString(string selection, string source, IDictionary<string, object> WhereParams)
        {
            string whereStr = " WHERE ";
            foreach (string Id in WhereParams.Keys)
            {
                whereStr += Id + "=" + Parameter(Id)+" AND ";
            }
            return QueryString(selection, source, whereStr.Substring(0, whereStr.Length - 5),"","");
        }

        public string QueryStringPage(string selection, string source, string conditions, string order, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public string QueryStringPage(string selection, string source, string conditions, string order, int pageSize)
        {
            throw new NotImplementedException();
        }

        public string Table(string tableName)
        {
            return string.Format("{0}", tableName);
        }

        public string UpdateString(string tableName, string primaryKeyName, Dictionary<string, string> fieldValues)
        {
            string res = "update " + tableName + " SET ";
            foreach (KeyValuePair<string, string> fieldValue in fieldValues)
            {
                res += (fieldValue.Key + " = " + fieldValue.Value + ",");
            }
            res = res.Substring(0, res.Length - 1);
            res += " WHERE " + primaryKeyName + " = " + fieldValues[primaryKeyName];
            return res;
        }
    }
}
