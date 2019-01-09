using System;
using System.Collections.Generic;

namespace OOPDPFinalProject.ORMFramework.SQLBuilder
{
    public class SQLSelection:SQLBuilder
    {

        public SQLSelection()
        {
        }
        public void AddTableName(string tableName)
        {
            _componentList.Add(string.Format("{0}", _adapter.Table(tableName)));
        }
        protected override string ComponentsToString()
        {
            return string.Join(", ", _componentList);
        }

        public string GetString(string tableName)
        {
            if (_componentList.Count == 0)
                return string.Format("{0}.*", _adapter.Table(tableName));
            else
                return string.Join(", ", _componentList);
        }

        public void AddSelect(string tableName, string fieldName)
        {
            _componentList.Add(_adapter.Field(tableName, fieldName));
        }

        public void AddSelect(string tableName, string fieldName, LambdaSqlBuilder.Resolver.SelectFunction selectFunction)
        {
            var selectionString = string.Format("{0}({1})", selectFunction.ToString(), _adapter.Field(tableName, fieldName));
            _componentList.Add(selectionString);
        }
    }
}
