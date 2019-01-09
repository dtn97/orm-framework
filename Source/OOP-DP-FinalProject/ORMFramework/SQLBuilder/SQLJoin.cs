using System;
namespace OOPDPFinalProject.ORMFramework.SQLBuilder
{
    public class SQLJoin:SQLBuilder
    {
        public SQLJoin()
        {
        }

        protected override string ComponentsToString()
        {
            return string.Join(" ", _componentList);
        }

        public void AddJoin(string originalTableName, string joinTableName, string leftField, string rightField)
        {
            _componentList.Add(string.Format("JOIN {0} ON {1} = {2}",
                   _adapter.Table(joinTableName),
                   _adapter.Field(originalTableName, leftField),
                   _adapter.Field(joinTableName, rightField)));
        }

    }
}
