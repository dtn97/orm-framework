using System;
namespace OOPDPFinalProject.ORMFramework.SQLBuilder
{
    public class SQLGrouping:SQLBuilder
    {
        public SQLGrouping()
        {
        }

        protected override string ComponentsToString()
        {
            return "GROUP BY " + string.Join(", ", _componentList);
        }

        public void AddGroupBy(string tableName, string fieldName)
        {
            _componentList.Add(_adapter.Field(tableName, fieldName));
        }
    }
}
