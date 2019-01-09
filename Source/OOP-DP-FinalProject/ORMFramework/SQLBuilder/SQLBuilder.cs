using System;
using System.Collections.Generic;

namespace OOPDPFinalProject.ORMFramework.SQLBuilder
{
    public abstract class SQLBuilder
    {   
        protected List<string> _componentList = new List<string>();
        protected static ISqlAdapter _adapter
        {
            get
            {
                return Session.getCurSession().SQLAdapter;
            }
        }
        protected abstract string ComponentsToString();
        public static string GetQueryString(SQLSelection Selection, SQLCondition Conditions, SQLGrouping Grouping, SQLHaving Having, SQLOrderBy Order, SQLJoin Join, string TableName)
        {
            var Source = string.Format("{0} {1}", _adapter.Table(TableName), Join.GetString());
            return _adapter.QueryString(Selection.GetString(), Source, Conditions.GetString(), Grouping.GetString(), Having.GetString(), Order.GetString());
        }
        public static string AddOffsetLimit (string sql, SQLOffsetLimit offsetLimit)
        {
            Console.WriteLine(string.Format("{0} {1}", sql, offsetLimit.getString()));
            return string.Format("{0} {1}", sql, offsetLimit.getString());
        }
        public virtual void Add(string component)
        {
            _componentList.Add(component);
        }

        public virtual string GetString()
        {
            if (_componentList.Count == 0)
            {
                return "";
            }
            return ComponentsToString();
        }
        public SQLBuilder()
        {
        }
    }
}
